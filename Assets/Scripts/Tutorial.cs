using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public GameObject panel1;
	public GameObject panel2;

	public void Panel2()
	{
		panel1.SetActive(false);
		panel2.SetActive(true);
	}

	public void Level1()
	{
		GameManager.Instance.LoadScene("Level1");
	}
}
