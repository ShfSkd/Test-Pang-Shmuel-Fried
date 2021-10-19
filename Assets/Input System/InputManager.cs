using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
	public delegate void OnStartTouch(Vector2 pos, float time);
	public event OnStartTouch onStartTouch;
	public delegate void OnEndTouch(Vector2 pos, float time);
	public event OnEndTouch onEndTouch;

	TouchControll touchControol;

	static InputManager instance;

	public static InputManager Instance
	{
		get { return instance; }
	}
	private void Awake()
	{
		touchControol = new TouchControll();
		if (instance != null)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	private void OnEnable()
	{
		touchControol.Enable();
	}
	private void OnDisable()
	{
		touchControol.Disable();
	}
	private void Start()
	{
		touchControol.Touch.TouchPress.started += ctx => StartTouch(ctx);
		touchControol.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
	}
	private void StartTouch(InputAction.CallbackContext context)
	{
		Debug.Log("Touch Started" + touchControol.Touch.TouchPosition.ReadValue<Vector2>());
		if (onStartTouch != null)
			onStartTouch(touchControol.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
	}

	private void EndTouch(InputAction.CallbackContext context)
	{
		Debug.Log("Tohch Ended" + touchControol.Touch.TouchPosition.ReadValue<Vector2>());
		if (onEndTouch != null)
			onEndTouch(touchControol.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
	}

}
