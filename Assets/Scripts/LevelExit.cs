using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			if(Player.Instance.hasSock)
			{
				Debug.Log("Victory!");
			}
			else
			{
				Debug.Log("Player entered level exit without gnome :(");
			}
		}
	}
}
