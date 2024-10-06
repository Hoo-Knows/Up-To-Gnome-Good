using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : InteractableObject
{
	protected override void OnInteract()
	{
		// Play a sound
		Debug.Log("Distracting closest person");
		transform.FindClosestPersonInVision()?.Distract(transform.position);
	}
}
