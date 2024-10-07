using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
	public static Player Instance;
	public bool hasSock;
	public bool savedGnome;
	public bool caught;
	public int throwables = 0;
	public List<Vector2> positionList = new List<Vector2>();

	[SerializeField] private GameObject _throwablePrefab;
	private bool _aiming;
	private float _speed = 5f;
	private float _positionListTimer;
	private float _positionListUpdateTime = 0.1f;
	private int _maxPositionListLength = 10;
	private Animator _animator;
	private Rigidbody2D _rb;
	private SpriteRenderer _sr;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}

	private void Start()
    {
		_animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
		_sr = GetComponent<SpriteRenderer>();
    }

	private bool FreezeMovement()
	{
		return GameManager.Instance.isPaused || caught;
	}

    private Vector2 GetMoveVector()
    {
		if(FreezeMovement()) return Vector2.zero;

		float moveX = 0f;
		float moveY = 0f;
		if(Input.GetKey(KeyCode.W))
		{
			moveY += 1f;
		}
		if(Input.GetKey(KeyCode.A))
		{
			moveX -= 1f;
		}
		if(Input.GetKey(KeyCode.S))
		{
			moveY -= 1f;
		}
		if(Input.GetKey(KeyCode.D))
		{
			moveX += 1f;
		}
		return new Vector2(moveX, moveY).normalized;
	}

	private void Update()
	{
		// Start/stop aiming
		if(Input.GetKeyDown(KeyCode.Q))
		{
			_aiming = !_aiming;
		}

		// Throw object
		if(_aiming && Input.GetMouseButtonDown(0) && throwables > 0)
		{
			// Spawn the throwable
			GameObject throwableGO = Instantiate(_throwablePrefab, transform.position, Quaternion.identity);
			throwableGO.SetActive(false);
			
			// Set the target and yeet
			Throwable throwable = throwableGO.GetComponent<Throwable>();
			throwable.target = transform.Find("Aim Sprite").GetComponent<AimSprite>().target;
			throwableGO.SetActive(true);

			// Stop aiming and remove a throwable
			_aiming = false;
			throwables--;
		}
		transform.Find("Aim Sprite").gameObject.SetActive(_aiming && !caught);

		// Handle movement
		Vector2 moveVector = GetMoveVector();
		_rb.velocity = _speed * moveVector;

		// Play animations
		_animator.SetBool("Walking", moveVector.magnitude > 0f);

		// Flip sprite if necessary
		if(moveVector.x > 0f)
		{
			_sr.flipX = true;
		}
		if(moveVector.x < 0f)
		{
			_sr.flipX = false;
		}

		// Track movement for gnomes to follow
		// Only track movement if player is moving
		if(moveVector.magnitude > 0f)
		{
			_positionListTimer += Time.deltaTime;
		}

		// Update list every _positionListUpdateTime seconds
		if(_positionListTimer > _positionListUpdateTime)
		{
			positionList.Add(transform.position.ToVector2());
			while(positionList.Count > _maxPositionListLength)
			{
				positionList.RemoveAt(0);
			}
			_positionListTimer = 0f;
		}
	}
}
