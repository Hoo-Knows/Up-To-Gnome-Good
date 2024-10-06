using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
	protected bool _canInteract;
	private GameObject _outline;

	private void Start()
	{
		_outline = transform.Find("Outline").gameObject;
	}

	private void Update()
	{
		_outline.SetActive(_canInteract);
		if(_canInteract && Input.GetKeyDown(KeyCode.Space))
		{
			OnInteract();
		}
	}

	protected abstract void OnInteract();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			_canInteract = true;
			Debug.Log(gameObject.name + " is interactable");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			_canInteract = false;
			Debug.Log(gameObject.name + " is not interactable");
		}
	}
}
