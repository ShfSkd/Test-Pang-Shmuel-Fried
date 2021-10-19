using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
	Camera camMain;

	private void Awake()
	{
		camMain = Camera.main;
	}
	private void OnEnable()
	{
		InputManager.Instance.onStartTouch += Move;
	}
	private void OnDisable()
	{
		InputManager.Instance.onEndTouch -= Move;
	}

	private void Move(Vector2 screenPosition, float time)
	{
		Debug.Log("1");
		Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, camMain.nearClipPlane);
		Vector3 worldCoordinates = camMain.ScreenToWorldPoint(screenCoordinates);
		worldCoordinates.z = 0;
		transform.position = worldCoordinates;
	}
}
