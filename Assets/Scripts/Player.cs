using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] GameObject weaponPrefab;
	[SerializeField] float offsetWeapon;
	[SerializeField] ParticleSystem hitEffect;

	[Header("Touch Settings")]
	[SerializeField] bool isUseTouch;
	[SerializeField] Button fireButton;  
	[SerializeField] Joystick joystick;

	[HideInInspector]
	public bool isAlive = true;

	Vector2 moveInput;
	Rigidbody2D rb;
	Animator anim;
	bool canWalk;
	public static bool canShoot;
	AudioPlayer audioPlayer;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		audioPlayer = FindObjectOfType<AudioPlayer>();
	}
	private void Start()
	{
		if (isUseTouch)
		{
			joystick.gameObject.SetActive(true);
			fireButton.gameObject.SetActive(true);
		}
		else
		{
			joystick.gameObject.SetActive(false);
			fireButton.gameObject.SetActive(false);
		}
		canWalk = true;
		canShoot = true;
	}
	private void Update()
	{
		if (!isAlive) return;

		HandleTouch();

		Run();
		FlipSprite();
	}

	public void Shoot()
	{
		if (isUseTouch)
		{
			if (canShoot)
			{
				canShoot = false;
				StartCoroutine(ShootWeapon());
			}
		}
	}
	private void HandleTouch()
	{
		if (isUseTouch)
		{
			if (joystick.Horizontal == 0)
			{
				if (anim != null)
					anim.SetBool(AnimationTags.IS_RUNINIG, false);
			}
			moveInput.x = joystick.Horizontal;

		}
	}

	private void Run()
	{
		if (canWalk)
		{
			Vector2 playerVerlocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
			rb.velocity = playerVerlocity;

			bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

			if (anim != null)
				anim.SetBool(AnimationTags.IS_RUNINIG, playerHasHorizontalSpeed);
		}


	}
	private void FlipSprite()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

		if (playerHasHorizontalSpeed)
		{
			transform.GetChild(0).localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
		}
	}
	private void OnMove(InputValue value)
	{
		if (!isAlive) return;
		if (isUseTouch) return;

		moveInput = value.Get<Vector2>();
	}
	void OnFire(InputValue value)
	{
		if (!isAlive) return;
		if (isUseTouch)return;

		if (value.isPressed)
		{
			if (canShoot)
			{
				canShoot = false;
				StartCoroutine(ShootWeapon());
			}
		}
	}
	private IEnumerator ShootWeapon()
	{
		canWalk = false;
		anim.SetBool(AnimationTags.IS_SHOOTING, true);
		audioPlayer.PlayShootingClip();

		Vector3 temp = transform.position;

		temp.y += transform.GetChild(0).position.y + offsetWeapon;

		GameObject weapon = Instantiate(weaponPrefab, temp, Quaternion.identity);

		yield return new WaitForSeconds(0.2f);
		anim.SetBool(AnimationTags.IS_SHOOTING, false);
		canWalk = true;

		yield return new WaitForSeconds(0.3f);
		canShoot = true;
	}
}
