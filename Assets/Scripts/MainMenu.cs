using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
