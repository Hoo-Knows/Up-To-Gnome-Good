using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : InteractableObject
{
	protected override void OnInteract()
	{
		Debug.Log("Player picked up sock");
		Player.Instance.hasSock = true;
		Destroy(gameObject);
	}
}
