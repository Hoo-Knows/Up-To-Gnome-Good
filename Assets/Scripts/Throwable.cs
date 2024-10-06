using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Vector2 target;

    private float _speed = 15f;

	private void Start()
	{
		StartCoroutine(Throw());
	}

    private IEnumerator Throw()
    {
		while(Vector2.Distance(transform.position.ToVector2(), target) > 0.05f)
		{
			transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.fixedDeltaTime);
			yield return null;
		}
		transform.position = target;
		Debug.Log("Throwable landed at " + target);
		transform.FindClosestPersonInVision()?.Distract(target);
	}
}
