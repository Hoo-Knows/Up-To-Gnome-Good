using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public float angle;

	[SerializeField] private float fov;
	[SerializeField] private int rayCount = 50;
	[SerializeField] private float distance;
	private Mesh mesh;

	private void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	private void Update()
	{
		// If we caught the player, stop updating the mesh
		if(Player.Instance.caught) return;

		// Create mesh fields
		Vector3[] vertices = new Vector3[rayCount + 2];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[rayCount * 3];

		// Initial angle from which to start raycasting
		float angle = this.angle + fov / 2f;
		float angleIncrease = fov / rayCount;

		// Indices for setting mesh fields
		int vertexIndex = 1;
		int triangleIndex = 0;

		// Starts at the fov location
		vertices[0] = Vector3.zero;

		// Only raycast for default or player
		string[] layers = { "Default", "Player" };

		// Raycast to produce mesh
		for(int i = 0; i < rayCount; i++)
		{
			// Find new vertex
			Vector3 vertex;
			Vector3 raycastVector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
			RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, raycastVector, distance, LayerMask.GetMask(layers));
			if(raycastHit.collider == null)
			{
				vertex = raycastVector.normalized * distance;
			}
			else
			{
				if(raycastHit.collider.CompareTag("Player"))
				{
					Player.Instance.caught = true;
					GameManager.Instance.LoadScene(GameManager.Instance.currentScene);
				}
				vertex = new Vector3(raycastHit.point.x, raycastHit.point.y) - transform.position;
			}
			vertices[vertexIndex] = vertex;

			// Create new triangle based on origin, previous raycast point, and current raycast point
			if(i > 0)
			{
				triangles[triangleIndex] = 0;
				triangles[triangleIndex + 1] = vertexIndex - 1;
				triangles[triangleIndex + 2] = vertexIndex;

				triangleIndex += 3;
			}
			vertexIndex++;
			angle -= angleIncrease;
		}
		
		// Set mesh fields
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		mesh.RecalculateBounds();
	}
}
