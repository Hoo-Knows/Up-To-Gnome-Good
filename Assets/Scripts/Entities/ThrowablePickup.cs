using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablePickup : InteractableObject
{
	protected override void OnInteract()
	{
		Debug.Log("Picked up a throwable");
		Player.Instance.throwables++;
		Destroy(gameObject);
	}
}
