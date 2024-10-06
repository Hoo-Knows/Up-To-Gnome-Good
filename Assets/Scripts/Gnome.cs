using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public bool saved;
	public int gnomeIndex;

    // Same speed as player
    private float _speed = 5f;

	private void Update()
	{
		if(saved)
		{
			// Interval that the list updates in is every 0.1f, so lag [gnomeIndex / 5] seconds behind player
			int positionIndex = Player.Instance.positionList.Count - gnomeIndex * 2;
			transform.position = Vector2.MoveTowards(transform.position, Player.Instance.positionList[positionIndex], _speed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Debug.Log("Gnome saved!");
			saved = true;
			Player.Instance.savedGnome = true;
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
