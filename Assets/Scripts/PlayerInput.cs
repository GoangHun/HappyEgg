using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public string mouseMoveName = "Mouse X";
	public float doubleClickDuration = 1f;

	public float MouseX { get; set; } = 0f;
    public bool DoubleClick { get; private set; } = false;

	public Vector3 startPos = Vector3.zero;
	public Vector3 endPos = Vector3.zero;
    public LayerMask layerMask;


    private Camera worldCam;
    private float lastClickTime = 0f;


    private void Awake()
    {
        worldCam = Camera.main;

    }

    private void Update()
	{
		if (GameManager.Instance != null && GameManager.Instance.IsGameover)
		{
			MouseX = 0f;
			return;
		}

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = worldCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
            {
                DoubleClick = lastClickTime + doubleClickDuration > Time.time;
                lastClickTime = Time.time;
            }
        }
        else
        {
            DoubleClick = false;
        }
    }

}
