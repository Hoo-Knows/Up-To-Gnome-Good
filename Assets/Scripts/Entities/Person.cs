using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public AudioClip alertSound;
    public GameObject alert;

    private Rigidbody2D _rb;
    private FieldOfView _fov;
    private SpriteRenderer _sr;

    private Vector3 _targetPos;
    private float _speed = 2.5f;
    private float _targetDistance = 1f;
    private Coroutine _moveTowardsTargetCoro;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
        _fov = GetComponentInChildren<FieldOfView>();
        _sr = GetComponent<SpriteRenderer>();

        _sr.flipX = FlipX();
	}

	public void Distract(Vector3 pos)
    {
        _targetPos = pos;
        if(_moveTowardsTargetCoro != null)
        {
            StopCoroutine(_moveTowardsTargetCoro);
            _moveTowardsTargetCoro = null;
        }
		_moveTowardsTargetCoro = StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget()
    {
		Debug.Log(gameObject.name + " distracted and moving towards " + _targetPos);

        // Surprised notif
        GameManager.Instance.PlaySFX(alertSound);
        alert.SetActive(true);
		_rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
		alert.SetActive(false);

		// Start moving, or at least turn towards it
		do
        {
            Vector3 moveVector = (_targetPos - transform.position).normalized;
            //Debug.Log(moveVector);

            // Set angle of fov
            _fov.angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            if(_fov.angle < 0f) _fov.angle += 360f;

            // Update sr flip
            _sr.flipX = FlipX();

			// Move
			_rb.velocity = moveVector * _speed;
            yield return null;
        }
        while(Vector3.Distance(transform.position, _targetPos) > _targetDistance && !Player.Instance.caught);

		// Stop moving
		_rb.velocity = Vector2.zero;
	}

    private bool FlipX()
    {
        return _fov.angle >= 90f && _fov.angle < 270f;

	}
}
