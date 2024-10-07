using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : InteractableObject
{
	public AudioClip sockPickupSound;

	protected override void OnInteract()
	{
		Debug.Log("Player picked up sock");
		Player.Instance.hasSock = true;
		GameManager.Instance.PlaySFX(sockPickupSound);
		Destroy(gameObject);
	}
}
