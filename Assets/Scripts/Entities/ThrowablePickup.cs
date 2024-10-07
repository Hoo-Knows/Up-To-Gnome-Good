using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablePickup : InteractableObject
{
	public AudioClip throwablePickupSound;

	protected override void OnInteract()
	{
		Debug.Log("Picked up a throwable");
		Player.Instance.throwables++;
		GameManager.Instance.PlaySFX(throwablePickupSound);
		Destroy(gameObject);
	}
}
