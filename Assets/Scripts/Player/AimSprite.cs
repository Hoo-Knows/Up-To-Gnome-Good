using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSprite : MonoBehaviour
{
	public float maxDistance;
	public Vector2 direction;
	public Vector2 target;

	private void Awake()
	{
		foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
		{
			sr.gameObject.SetActive(false);
		}
	}

	private void Update()
    {
		if(GameManager.Instance.isPaused) return;

		// Set direction towards mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		direction = (mousePos2D - transform.position.ToVector2()).normalized;

		// Raycast to figure out how far we can throw the object
		LayerMask layerMask = LayerMask.GetMask(new string[] { "Default", "NPC" });
		RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, 1000f, layerMask);

		//Debug.DrawLine(transform.position, raycastHit.point, Color.red);

		// Only use the raycast result if it's within max distance
		if(Vector2.Distance(raycastHit.point, transform.position.ToVector2()) < maxDistance)
		{
			target = raycastHit.point;
		}
		else
		{
			target = transform.position.ToVector2() + direction * maxDistance;
		}
		//Debug.DrawLine(transform.position, target, Color.green);

		// Loop through children and set their positions towards the direction
		for(int i = 0; i < transform.Find("Trail").childCount; i++)
		{
			Transform child = transform.Find("Trail").GetChild(i);
			// Set position of child
			child.localPosition = direction * 0.5f * (i + 1);
			// Only show child if it's before the target
			child.gameObject.SetActive(child.localPosition.magnitude - 0.01f <= Vector2.Distance(target, transform.position.ToVector2()));
		}
		transform.Find("Target").position = target;
		transform.Find("Target").gameObject.SetActive(true);
	}
}
