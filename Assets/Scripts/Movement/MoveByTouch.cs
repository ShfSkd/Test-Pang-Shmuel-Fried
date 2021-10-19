using UnityEngine;
using UnityEngine.InputSystem;

public class MoveByTouch : MonoBehaviour
{
	public Joystick joystick;

	float horizontal;
	private void Update()
	{
		horizontal = joystick.Horizontal * 100;
	}
}