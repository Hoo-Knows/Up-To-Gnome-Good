using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject[] gnomePrefabs;
	private int _gnomeIndex;
	private float _gnomeSpawnTime = 1.5f;
	private float _timer;

	private void Update()
	{
		_timer += Time.deltaTime;
		if(_timer > _gnomeSpawnTime)
		{
			GameObject gnome = Instantiate(gnomePrefabs[_gnomeIndex], transform);
			// Generates either -1f or 1f
			gnome.AddComponent<MainMenuGnomes>().moveDirection = 2f * Random.Range(0, 2) - 1f;

			_gnomeIndex++;
			if(_gnomeIndex >= gnomePrefabs.Length) _gnomeIndex = 0;
			_timer = 0f;

			Destroy(gnome, 10f);
		}
	}

	public void Play()
	{
		if(GameManager.Instance.showedTutorial)
		{
			GameManager.Instance.LoadScene("Level1");
		}
		else
		{
			GameManager.Instance.LoadScene("Tutorial");
		}
	}

	public void Options()
	{
		GameManager.Instance.MainToOptions();
	}
}
