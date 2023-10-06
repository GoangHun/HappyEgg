using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	public string fireButtonName = "Fire1";
	public string mouseMoveName = "Mouse X";

	public float MouseX { get; set; } = 0f;
	public bool MouseButton { get; private set; } = false;
	public bool Fire { get; private set; } = false;

	public Vector3 startPos = Vector3.zero;
	public Vector3 endPos = Vector3.zero;

	// 매프레임 사용자 입력을 감지
	private void Update()
	{
		if (GameManager.Instance != null && GameManager.Instance.IsGameover)
		{
			MouseX = 0f;
			MouseButton = false;
			return;
		}

		MouseButton = Input.GetMouseButton(0);
		Fire = Input.GetButton(fireButtonName);
	}
}
