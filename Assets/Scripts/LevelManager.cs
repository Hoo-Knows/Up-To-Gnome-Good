using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
	public bool finishedLoading = true;

	private Animator _animator;
	[SerializeField] private int _level;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		_level = 1;
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			RetryLevel(0f);
		}
	}

	public void RetryLevel(float delay)
	{
		LoadLevel("Level" + _level, delay);
	}

	public void LoadNextLevel(float delay)
	{
		_level++;
		LoadLevel("Level" + _level, delay);
	}

	public void LoadLevel(string sceneName, float delay)
	{
		if(!finishedLoading) return;

		StartCoroutine(LoadLevelCoro(sceneName, delay));
	}

	public IEnumerator LoadLevelCoro(string sceneName, float delay)
	{
		Debug.Log("Loading scene " + sceneName);
		finishedLoading = false;
		yield return new WaitForSeconds(delay);
		_animator.Play("FadeOut");
		yield return null;
		yield return new WaitWhile(() => _animator.IsPlaying());
		SceneManager.LoadScene(sceneName);
		_animator.Play("FadeIn");
		yield return new WaitWhile(() => _animator.IsPlaying());
		finishedLoading = true;
	}
}
