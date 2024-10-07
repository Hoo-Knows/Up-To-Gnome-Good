using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
	public TMP_Text statsText;
	public GameObject socks;
	public Image stage;

	private int _sockCounter;
	private float _hue;

	private void Awake()
	{
		_sockCounter = 0;
		for(int i = 0; i < GameManager.Instance.socksStolen.Length; i++)
		{
			if(GameManager.Instance.socksStolen[i]) _sockCounter++;
			socks.transform.GetChild(i).gameObject.SetActive(GameManager.Instance.socksStolen[i]);
		}
		if(_sockCounter == 0)
		{
			statsText.text = "You saved your friends, but didn't get any socks";
		}
		else if(_sockCounter > 0 && _sockCounter < 4)
		{
			statsText.text = string.Format("You saved your friends and stole {0} {1}", _sockCounter, _sockCounter == 1 ? "sock" : "socks");
		}
        else if(_sockCounter == 4)
        {
			statsText.text = "You saved your friends and stole all the socks!";
		}

		// Dance if you got a sock
		if(_sockCounter > 0)
		{
			StartCoroutine(GnomeDance());
		}
		else
		{
			foreach(Animator animator in GetComponentsInChildren<Animator>())
			{
				Destroy(animator);
			}
		}
	}

	private IEnumerator GnomeDance()
	{
		while(isActiveAndEnabled)
		{
			yield return new WaitForSeconds(1f);
			foreach(Animator animator in GetComponentsInChildren<Animator>())
			{
				RectTransform rectTransform = animator.GetComponent<RectTransform>();
				Vector3 scale = rectTransform.localScale;
				rectTransform.localScale = new Vector3(-scale.x, scale.y, scale.z);
			}
		}
	}

	private void Update()
	{
		if(_sockCounter >= 3)
		{
			_hue += Time.deltaTime * 0.25f;
			_hue = _hue % 1;
			stage.color = Color.HSVToRGB(_hue, 0.7f, 0.6f);
		}
	}

	public void MainMenu()
	{
		GameManager.Instance.LoadScene("MainMenu");
	}
}
