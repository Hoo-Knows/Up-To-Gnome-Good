using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;
	public bool hasSock;
	public bool caught;
	public int throwables = 0;

	[SerializeField] private GameObject _throwablePrefab;
	private bool _aiming;
	private float _speed = 5f;
	private Rigidbody2D _rb;

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
        _rb = GetComponent<Rigidbody2D>();
    }

	private bool FreezeMovement()
	{
		return !LevelManager.Instance.finishedLoading || caught;
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
		if(Input.GetKeyDown(KeyCode.Q))
		{
			_aiming = !_aiming;
		}
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
		_rb.velocity = _speed * GetMoveVector();
	}
}
