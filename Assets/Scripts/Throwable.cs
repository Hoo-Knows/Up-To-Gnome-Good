using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	private Vector2 origin;
    public Vector2 target;

    private float _speed = 15f;

	private void Start()
	{
		origin = transform.position.ToVector2();
		StartCoroutine(Throw());
	}

    private IEnumerator Throw()
    {
		while(Vector2.Distance(transform.position.ToVector2(), target) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.fixedDeltaTime);
			yield return null;
		}
		// Fall a little bit short so we don't get errors when raycasting to find the nearest person
		Vector2 offset = (origin - target).normalized * 0.1f;
		transform.position = target + offset;
		Debug.Log("Throwable landed at " + transform.position);
		transform.FindClosestPersonInVision()?.Distract(target);
	}
}
