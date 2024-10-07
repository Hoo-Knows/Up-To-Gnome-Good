using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGnomes : MonoBehaviour
{
	public float moveDirection;
    private RectTransform _rectTransform;
	private Vector3 _moveVector;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();

		// Flip sprite
		Vector3 localScale = _rectTransform.localScale;
		_rectTransform.localScale = new Vector3(-1f * moveDirection * localScale.x, localScale.y, localScale.z);

		// Set position
		float x = MainMenu.canvasRect.width / 2f * (1f - moveDirection) - moveDirection * 100f;
		float y = Random.Range(200f, MainMenu.canvasRect.height - 200f);
		_rectTransform.position = new Vector3(x, y, 0f);

		// Set move vector
		_moveVector = new Vector3(Random.Range(600f, 1000f) * moveDirection, 0f, 0f);
	}

	void Update()
    {
		_rectTransform.position += Time.deltaTime * _moveVector;
    }
}
