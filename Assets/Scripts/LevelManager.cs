using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	[SerializeField] float sceneLoadDealy = 2f;

	ScoreKeeper scoreKeeper;

	private void Awake()
	{
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}
	public void LoadGame()
	{
		scoreKeeper.ResetScore();
		SceneManager.LoadScene(ScenesTags.Level_1);
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
