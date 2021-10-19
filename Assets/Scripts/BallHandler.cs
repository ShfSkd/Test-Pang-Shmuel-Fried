using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
	[SerializeField] bool moveLeft, moveRight;
	[SerializeField] int score = 50;
	[SerializeField] GameObject originalBall;

	float forceX, forceY;
    GameObject ball1, ball2;
    BallHandler ballHandler1, ballHandler2;
    Rigidbody2D rb;
	ScoreKeeper scoreKeeper;
	AudioPlayer audioPlayer;

	private void Awake()
	{
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
		rb = GetComponent<Rigidbody2D>();
		audioPlayer = FindObjectOfType<AudioPlayer>();
	}
	private void Start()
	{
		SetBallSpeed();
	}
	private void Update()
	{
		MoveBall();
	}
	void InstantiateBalls()
	{
		if (!gameObject.CompareTag(Tags.SMALL_BALL))
		{
			ball1 = Instantiate(originalBall);
			ball2 = Instantiate(originalBall);

			ball1.name = originalBall.name;
			ball2.name = originalBall.name;

			ballHandler1 = ball1.GetComponent<BallHandler>();
			ballHandler2 = ball2.GetComponent<BallHandler>();
		}
	}
	void ChangeBallsWhenHit()
	{
		InstantiateBalls();

		Vector3 temp = transform.position;

		ball1.transform.position = temp;
		ballHandler1.SetMoveLeft(true);

		ball2.transform.position = temp;
		ballHandler2.SetMoveRight(true);

		ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
		ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);

		gameObject.SetActive(false);
	}
	public void SetMoveLeft(bool canMoveLeft)
	{
		moveLeft = canMoveLeft;
		moveRight = !canMoveLeft;
	}
	public void SetMoveRight(bool canMoveRigth)
	{
		moveRight = canMoveRigth;
		moveLeft = !canMoveRigth;
	}
	void MoveBall()
	{
		if (moveLeft)
		{
			Vector3 temp = transform.position;
			temp.x -= forceX * Time.deltaTime;
			transform.position = temp;
		}
		if (moveRight)
		{
			Vector3 temp = transform.position;
			temp.x += forceX * Time.deltaTime;
			transform.position = temp;
		}
	}
	void SetBallSpeed()
	{
		forceX = 2.5f;
		switch (gameObject.tag)
		{
			case (Tags.BIGGEST_BALL):
				forceY = 10.5f;
				break;
			case (Tags.BIG_BALL):
				forceY = 9.5f;
				break;
			case (Tags.MEDUIM_BALL):
				forceY = 8.5f;
				break;
			case (Tags.SMALL_BALL):
				forceY = 6.5f;
				break;
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(Tags.GROUND))
		{
			rb.velocity = new Vector2(0, forceY);
		}
		if (other.gameObject.CompareTag(Tags.RIGTHWALL))
		{
			SetMoveLeft(true);
		}
		if (other.gameObject.CompareTag(Tags.LEFTWALL))
		{
			SetMoveRight(true);
		}
		if (other.gameObject.CompareTag(Tags.WEAPON))
		{
			if (!gameObject.CompareTag(Tags.SMALL_BALL))
			{
				ChangeBallsWhenHit();
				scoreKeeper.ModifyScore(score);
				audioPlayer.PlayDamageClip();
			}
			else
			{
				gameObject.SetActive(false); 
				scoreKeeper.ModifyScore(score);
				audioPlayer.PlayDamageClip();
			}
		}
	}
	

}
