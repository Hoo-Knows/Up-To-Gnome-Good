using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSortLayer : MonoBehaviour
{
    private SpriteRenderer _sr;

	private void Start()
	{
        _sr = GetComponent<SpriteRenderer>();
	}

	void Update()
    {
        if(Player.Instance.transform.position.y > transform.position.y)
        {
            _sr.sortingLayerName = "Front";
        }
        else
        {
            _sr.sortingLayerName = "Behind";
        }
    }
}
