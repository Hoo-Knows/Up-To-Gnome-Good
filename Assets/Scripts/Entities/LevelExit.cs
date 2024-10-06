using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			if(Player.Instance.savedGnome)
			{
				Debug.Log("Victory!");
				// Save sock data to GM
				GameManager.Instance.socksStolen[GameManager.Instance.level - 1] = Player.Instance.hasSock;
				if(GameManager.Instance.level == 4)
				{
					GameManager.Instance.LoadScene("Victory");
				}
				else
				{
					GameManager.Instance.LoadScene("Level" + (GameManager.Instance.level + 1));
				}
			}
			else
			{
				Debug.Log("Player entered level exit without gnome, nothing happened :(");
			}
		}
	}
}
