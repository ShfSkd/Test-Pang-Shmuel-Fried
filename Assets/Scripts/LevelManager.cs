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
		SceneManager.LoadScene("Level 1");
		scoreKeeper.ResetScore();
	}
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void LoadGameOver()
	{
		StartCoroutine(WaitAndLoad("Game Over", sceneLoadDealy));
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
