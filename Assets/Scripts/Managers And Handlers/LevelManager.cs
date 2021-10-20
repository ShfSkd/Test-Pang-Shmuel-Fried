using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField] float sceneLoadDealy = 2f;
	[SerializeField] Image winPanel;

	ScoreKeeper scoreKeeper;

	BallHandler[] ballHandlers;

	static int currentLevel = 1;

	public int ballsCount;

	private void Awake()
	{
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}
	private void Start()
	{
		Time.timeScale = 1;

		if (winPanel != null)
			winPanel.gameObject.SetActive(false);

		ballHandlers = FindObjectsOfType<BallHandler>();
		for (int i = 0; i < ballHandlers.Length; i++)
		{
			ballsCount += ballHandlers[i].GetBallsCount();
		}
	}
	private void Update()
	{
		NextLevel();
	}
	public void NextLevel()
	{
		if (ballsCount <= 0 && winPanel != null)
		{
			Time.timeScale = 0;
			winPanel.gameObject.SetActive(true);
		}
	}
	public void LoadNextLevel()
	{
		int index = SceneManager.GetActiveScene().buildIndex;
		if (!(SceneManager.sceneCount < index + 1))
		{

			currentLevel++;
			SceneManager.LoadScene(index + 1);
		}
		else
		{
			SceneManager.LoadScene(ScenesTags.MAIN_MENU);
		}
	}
	public void LoadGame()
	{
		scoreKeeper.ResetScore();
		SceneManager.LoadScene(ScenesTags.Level_1);
	}
	public void RetryButton()
	{
		SceneManager.LoadScene(currentLevel);
	}
	public void LoadMainMenu()
	{
		SceneManager.LoadScene(ScenesTags.MAIN_MENU);
	}
	public void LoadGameOver()
	{
		StartCoroutine(WaitAndLoad(ScenesTags.GAME_OVER, sceneLoadDealy));
	}
	public void QuitGame()
	{
		Debug.Log("Quiting..");
		Application.Quit();
	}
	IEnumerator WaitAndLoad(string sceneName,float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneName);
	}
}
