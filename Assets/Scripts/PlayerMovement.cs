using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {
    private Transform playerTransform;
    private int currentTransIndex;

	public Transform[] lineTransforms;
    public LayerMask layerMask;

	private void Awake()
	{
		playerTransform = GetComponent<Transform>();
		currentTransIndex = 1;
	}

    private void FixedUpdate() {
		Move();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			--currentTransIndex;
			currentTransIndex = currentTransIndex < 0 ? 2 : currentTransIndex;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			++currentTransIndex;
			currentTransIndex = currentTransIndex % 3;
		}
	}

	private void Move() {
		playerTransform.position = lineTransforms[currentTransIndex].position;
	}
}