using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : InteractableObject
{
	public AudioClip distractionSound;

	protected override void OnInteract()
	{
		Debug.Log("Distracting closest person");
		transform.FindClosestPersonInVision()?.Distract(transform.position);
		GameManager.Instance.PlaySFX(distractionSound);
	}
}
