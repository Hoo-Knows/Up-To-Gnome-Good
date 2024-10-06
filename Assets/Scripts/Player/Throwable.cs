using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Vector2 target;
	public Sprite normal;
	public Sprite broken;

	private Vector2 _origin;
    private float _speed = 10f;
    private float _rotSpeed = 720f;

	private void Start()
	{
		_origin = transform.position.ToVector2();
		StartCoroutine(Throw());
	}

    private IEnumerator Throw()
    {
		while(Vector2.Distance(transform.position.ToVector2(), target) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.fixedDeltaTime);
			transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + _rotSpeed * Time.fixedDeltaTime);
			yield return null;
		}
		// Fall a little bit short so we don't get errors when raycasting to find the nearest person
		Vector2 offset = (_origin - target).normalized * 0.1f;
		transform.position = target + offset;
		transform.rotation = Quaternion.identity;
		GetComponent<SpriteRenderer>().sprite = broken;

		Debug.Log("Throwable landed at " + transform.position);
		transform.FindClosestPersonInVision()?.Distract(target);
	}
}
