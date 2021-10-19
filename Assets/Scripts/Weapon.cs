using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float speed = 5f;
	
    Rigidbody2D rb;
	BoxCollider2D weaponCollider;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		weaponCollider = GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
		if (!weaponCollider.IsTouchingLayers(LayerMask.GetMask("Border")))
		{
			rb.velocity = new Vector2(0, speed);
		}
		else
		{

			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<BallHandler>())
		{
			Destroy(gameObject);
		}
	}
}
