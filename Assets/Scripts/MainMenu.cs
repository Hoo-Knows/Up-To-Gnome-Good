using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void Play()
	{
		GameManager.Instance.LoadScene("Level1");
	}

	public void Options()
	{
		GameManager.Instance.MainToOptions();
	}
}
