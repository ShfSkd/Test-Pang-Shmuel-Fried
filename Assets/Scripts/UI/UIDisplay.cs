using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
	[Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


	private void Awake()
	{
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}
	private void Update()
	{
		scoreText.text = scoreKeeper.GetScore().ToString("00000");
	}
}
