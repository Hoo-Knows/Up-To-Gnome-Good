using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public bool saved;

    // Same speed as player
    private float _speed = 5f;
	private SpriteRenderer _sr;
	private Animator _animator;

	private void Start()
	{
		_sr = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if(saved)
		{
			// Interval that the list updates in is every 0.1s, so lag 0.2 seconds behind player
			int positionIndex = Player.Instance.positionList.Count - 2;
			if(positionIndex < 0) positionIndex = 0;
			Vector2 target = Vector2.MoveTowards(transform.position, Player.Instance.positionList[positionIndex], _speed * Time.deltaTime);
			Vector2 moveVector = target - transform.position.ToVector2();

			// Flip sr
			if(moveVector.x < 0)
			{
				_sr.flipX = false;
			}
			if(moveVector.x > 0)
			{
				_sr.flipX = true;
			}

			// Set animation
			_animator.SetBool("Walking", moveVector.magnitude > 0);

			transform.position = target;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Debug.Log("Gnome saved!");
			saved = true;
			Player.Instance.savedGnome = true;
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
