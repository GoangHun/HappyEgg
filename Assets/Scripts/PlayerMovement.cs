using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {

	public AudioClip jumpSound;
	public Transform[] lineTransforms;
	public float jumpForce = 200f;
	public float swaipDistance;

	private Rigidbody rgd;
	private Transform playerTransform;
    private int currentTransIndex;
	private PlayerInput playerInput;
	private bool isJump = false;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Block"))
		{
			isJump = false;
		}
	}

	private void Awake()
	{
		rgd = GetComponent<Rigidbody>();
		playerTransform = GetComponent<Transform>();
		playerInput = GetComponent<PlayerInput>();
		currentTransIndex = 1;
	}

	private void Update()
	{
		if (!GameManager.Instance.IsGameover)
		{
			if (Input.GetMouseButtonDown(0))
			{
                playerInput.startPos = Input.mousePosition;
            }
			if (Input.GetMouseButtonUp(0))
			{
				playerInput.endPos = Input.mousePosition;
				if (playerInput.startPos == Vector3.zero || swaipDistance > Vector3.Distance(playerInput.endPos, playerInput.startPos))
					return;

				var vec = playerInput.endPos - playerInput.startPos;
				var vecX = Mathf.Abs(vec.x);
				var vecY = Mathf.Abs(vec.y);

				if (!isJump && vecX > vecY)
				{
					if (ObstacleManager.Instance.IsMushroom)
						vec.x = -vec.x;

					if (vec.x > 0)
					{
						++currentTransIndex;
						currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
						Move();
					}
					else if (vec.x < 0)
					{
						--currentTransIndex;
						currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
						Move();
					}
				}
				else if (!isJump && vec.y > 0) 
				{
                    Jump();
                }
			}

            #region Test Code
#if UNITY_EDITOR
            if (ObstacleManager.Instance.IsMushroom)
			{
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					if (isJump)
						return;
					--currentTransIndex;
					currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
					Move();
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					if (isJump)
						return;
					++currentTransIndex;
					currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
					Move();
				}
				if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
				{
					if (!isJump)
					{
						Jump();
					}
				}

			}
			else
			{
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					if (isJump)
						return;
					--currentTransIndex;
					currentTransIndex = currentTransIndex < 0 ? 0 : currentTransIndex;
					Move();
				}
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					if (isJump)
						return;
					++currentTransIndex;
					currentTransIndex = currentTransIndex > 2 ? 2 : currentTransIndex;
					Move();
				}
				if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
				{
					if (!isJump)
					{
						rgd.velocity = Vector3.zero;
						rgd.AddForce(Vector2.up * jumpForce);
						isJump = true;
					}
				}
			}
#endif
            #endregion
        }
    }

	private void Move() {
		playerTransform.position = lineTransforms[currentTransIndex].position;
	}

	private void Jump()
	{
		rgd.velocity = Vector3.zero;
		rgd.AddForce(Vector2.up * jumpForce);
		AudioManager.instance.PlaySE(jumpSound);
		isJump = true;
	}
}