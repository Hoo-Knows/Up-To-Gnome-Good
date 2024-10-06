using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
	public TMP_Text statsText;

	private void Start()
	{
		int sockCounter = 0;
		foreach(bool sockStolen in GameManager.Instance.socksStolen)
		{
			if(sockStolen) sockCounter++;
		}
		statsText.text = string.Format("You saved your friends and helped steal {0} socks", sockCounter);
	}

	public void MainMenu()
	{
		GameManager.Instance.LoadScene("MainMenu");
	}
}
