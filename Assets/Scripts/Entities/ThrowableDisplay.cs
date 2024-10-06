using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowableDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text wineGlassText;

	private void Update()
	{
		wineGlassText.text = "Wine Glasses: " + Player.Instance.throwables;
	}
}
