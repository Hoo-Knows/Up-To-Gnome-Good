using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	public string currentScene;
	public bool isPaused;
	public int level = 1;
	public bool[] socksStolen = new bool[4];
	public bool showedTutorial;

	private bool _canPressPause = true;
	private bool _doingSceneTransition;

	[SerializeField] private AudioMixer _mixer;
	[SerializeField] private Animator _sceneTransitionAnimator;
	[SerializeField] private Animator _menuAnimator;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private GameObject _optionsMenu;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _sfxSlider;
	[SerializeField] private AudioSource _sfxSource;

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

	private void Update()
	{
		// Restart level
		if(Input.GetKeyDown(KeyCode.R) && !isPaused)
		{
			LoadScene(currentScene);
		}

		// Pause menu
		if(_canPressPause && Input.GetKeyDown(KeyCode.Escape) && currentScene.Contains("Level"))
		{
			if(!isPaused)
			{
				Pause();
			}
			else
			{
				Resume();
			}
		}
	}

	public void PlaySFX(AudioClip clip)
	{
		_sfxSource.PlayOneShot(clip);
	}

	public void SetMusicVolume(float volume)
	{
		_mixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
	}

	public void SetSFXVolume(float volume)
	{
		_mixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
	}

	public void Pause()
	{
		IEnumerator PauseCoro()
		{
			// Break if can't pause
			if(!_canPressPause) yield break;

			Debug.Log("Pausing");
			// Pause the game, disable player from unpausing until done with animations
			isPaused = true;
			Time.timeScale = 0f;
			_canPressPause = false;
			yield return MenuOverlayIn();
			_pauseMenu.SetActive(true);
			_optionsMenu.SetActive(false);

			// Can unpause again
			_canPressPause = true;
		}
		StartCoroutine(PauseCoro());
	}

	public void Resume()
	{
		IEnumerator ResumeCoro()
		{
			// Break if can't unpause
			if(!_canPressPause) yield break;

			Debug.Log("Unpausing");
			_canPressPause = false;
			_pauseMenu.SetActive(false);
			_optionsMenu.SetActive(false);
			yield return MenuOverlayOut();

			// Can pause again
			_canPressPause = true;
			isPaused = false;
			Time.timeScale = 1f;
		}
		StartCoroutine(ResumeCoro());
	}

	public void MainToOptions()
	{
		IEnumerator MainToOptions()
		{
			yield return MenuOverlayIn();
			_optionsMenu.SetActive(true);
		}
		StartCoroutine(MainToOptions());
	}

	public void PauseToOptions()
	{
		Debug.Log("Going to options menu from pause menu");
		_pauseMenu.SetActive(false);
		_optionsMenu.SetActive(true);
	}

	public void ExitOptions()
	{
		if(currentScene == "MainMenu")
		{
			Debug.Log("Close main menu options");
			_optionsMenu.SetActive(false);
			StartCoroutine(MenuOverlayOut());
		}
		else
		{
			Debug.Log("Returning to pause menu");
			_pauseMenu.SetActive(true);
			_optionsMenu.SetActive(false);
		}
	}

	public void PauseToMain()
	{
		_pauseMenu.SetActive(false);
		_optionsMenu.SetActive(false);
		StartCoroutine(MenuOverlayOut());
		LoadScene("MainMenu");
		Time.timeScale = 1f;
	}

	private IEnumerator MenuOverlayIn()
	{
		_menuAnimator.gameObject.SetActive(true);
		yield return _menuAnimator.PlayAndWait("MenuFadeIn");
	}
	
	private IEnumerator MenuOverlayOut()
	{
		yield return _menuAnimator.PlayAndWait("MenuFadeOut");
		_menuAnimator.gameObject.SetActive(false);
	}

	public void LoadScene(string sceneName)
	{
		if(_doingSceneTransition) return;

		_doingSceneTransition = true;
		_canPressPause = false;
		StartCoroutine(LoadSceneCoro(sceneName));
	}

	private IEnumerator LoadSceneCoro(string sceneName)
	{
		Debug.Log("Loading scene " + sceneName);
		// Set level num
		if(sceneName.Contains("Level"))
		{
			level = int.Parse(sceneName.Substring(5));
		}
		// Reset data when quitting to main menu
		if(sceneName == "MainMenu")
		{
			socksStolen = new bool[4];
		}

		currentScene = sceneName;
		isPaused = true;
		yield return _sceneTransitionAnimator.PlayAndWait("FadeOut");
		SceneManager.LoadScene(sceneName);
		yield return new WaitForSeconds(0.25f);
		yield return _sceneTransitionAnimator.PlayAndWait("FadeIn");
		isPaused = false;
		_canPressPause = true;
		_doingSceneTransition = false;
	}
}
