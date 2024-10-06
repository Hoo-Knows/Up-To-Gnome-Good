using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static bool IsPlaying(this Animator anim)
	{
		return anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f;
	}

	public static Person FindClosestPersonInVision(this Transform transform)
	{
		Person[] people = GameObject.FindObjectsOfType<Person>();
		if(people.Length <= 0) return null;

		Person closest = null;
		foreach(Person person in people)
		{
			// Raycast to check if the person has a line of sight
			LayerMask layerMask = LayerMask.GetMask(new string[] { "Default", "NPC" });
			RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, person.transform.position - transform.position, Mathf.Infinity, layerMask);
			//Debug.Log(raycastHit.collider.gameObject);
			if(raycastHit.collider != null && raycastHit.collider.GetComponent<Person>() == person)
			{
				// Set closest if this is the first person to be found
				if(closest == null)
				{
					closest = person;
					continue;
				}

				// Otherwise check if this person is closer
				if(Vector3.Distance(person.transform.position, transform.position) <
				Vector3.Distance(closest.transform.position, transform.position))
				{
					closest = person;
				}
			}
		}
		Debug.Log("Closest person is " + closest.gameObject);
		return closest;
	}

	public static Vector2 ToVector2(this Vector3 vector3)
	{
		return new Vector2(vector3.x, vector3.y);
	}
}
