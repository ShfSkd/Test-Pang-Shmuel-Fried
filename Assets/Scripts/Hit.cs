using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
	[SerializeField] ParticleSystem hitEffect;
	[SerializeField] Vector2 dethKick = new Vector2(10, 10);
	[SerializeField] bool isPlayer;
	Player player;

	LevelManager levelManager;
	AudioPlayer audioPlayer;

	private void Awake()
	{
		player = GetComponent<Player>();
		levelManager = FindObjectOfType<LevelManager>();
		audioPlayer = FindObjectOfType<AudioPlayer>();
	}

	void PlayHitEffect()
	{
		if (hitEffect != null)
		{
			ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		BallHandler ball = other.GetComponent<BallHandler>();

		if (ball != null)
		{
			audioPlayer.PlayDamageClip();
			PlayHitEffect();
			Die();
		}
		
	}

	private void Die()
	{
		if (isPlayer)
		{
			Animator animator = GetComponentInChildren<Animator>();
			Rigidbody2D rb = GetComponentInChildren<Rigidbody2D>();

			player.isAlive = false;
			rb.velocity = dethKick;
			animator.SetTrigger(AnimationTags.DIE);
			levelManager.LoadGameOver();
		}
	}
}
