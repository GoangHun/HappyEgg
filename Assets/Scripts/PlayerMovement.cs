using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {
    private Transform playerTransform;
    private int currentTransIndex;
	private PlayerInput playerInput;


	public Transform[] lineTransforms;


	private void Awake()
	{
		playerTransform = GetComponent<Transform>();
		playerInput = GetComponent<PlayerInput>();
		currentTransIndex = 1;
	}

    private void FixedUpdate() {
		Move();
	}

	private void Update()
	{
		if (!GameManager.Instance.IsGameover)
		{
			if (Input.GetMouseButtonDown(0))
				playerInput.startPos = Input.mousePosition;
			if (Input.GetMouseButtonUp(0))
			{
				playerInput.endPos = Input.mousePosition;
				var vec = playerInput.endPos.x - playerInput.startPos.x;

				if (ObstacleManager.Instance.IsMushroom)
					vec = -vec;

				if (vec > 0)
				{
					++currentTransIndex;
					currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
				}
				else if (vec < 0)
				{
					--currentTransIndex;
					currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
				}
			}

			//test code
			if (ObstacleManager.Instance.IsMushroom)
			{
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					--currentTransIndex;
					currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					++currentTransIndex;
					currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					--currentTransIndex;
					currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
				}
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					++currentTransIndex;
					currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
				}
			}
			
		}
	}

	private void Move() {
		playerTransform.position = lineTransforms[currentTransIndex].position;
	}
}