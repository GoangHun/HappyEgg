using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Conveyor : MonoBehaviour
{
	public float speed = 0f;
	public List<Block> blocks = new List<Block>();
	public Block lastBlock;
	public Player player;

	private float defaultSpeed;
	private float blockHeight;
	private Coroutine speedBuffCoroutine;
	private Coroutine speedDebuffCoroutine; // ���� ���� ���� �ڷ�ƾ�� ������ ����


	private void Awake()
	{
		blockHeight = blocks[0].transform.localScale.z;
		defaultSpeed = speed;
	}

	public void OnTriggerExit(Collider collision)
	{
		if (collision.CompareTag("Block"))
		{
			
			var target = collision.GetComponent<Block>();

			if (SpawnManager.Instance.IsEnd)
			{
				if (!lastBlock.gameObject.activeSelf)
				{
					lastBlock.gameObject.SetActive(true);   //4��° �� ��ġ�� ��ġ�� ���� ��Ȱ��ȭ ���ѳ���
					blocks.Add(lastBlock);
				}
				//target.Clear();
				return;
			}

			var pos = new Vector3(0, 0, blockHeight * blocks.Count);
			target.transform.position += pos;

			if (GameManager.Instance.currentStage == Stage.None)
				return;
			else if (GameManager.Instance.currentStage == Stage.Challenge)
			{
				target.Clear();
				target.CreateItems();
				target.CreateObstacles();
				target.CreateSocreItems();
			}
			else
			{
				target.Clear();
				target.SetObject();
			}
        }
	}

	void Update()
	{
		foreach (var block in blocks)
		{
			block.transform.position += Vector3.back * speed * Time.deltaTime;
		}
	}

	public void SpeedBuff(float speed, float time)
	{
		if (speedDebuffCoroutine != null)
		{
			StopCoroutine(speedDebuffCoroutine);
			speedDebuffCoroutine = null;
		}	
		if (speedBuffCoroutine != null)
			StopCoroutine(speedBuffCoroutine); 
		speedBuffCoroutine = StartCoroutine(SpeedBuffCoroutine(speed, time)); 
	}
	private IEnumerator SpeedBuffCoroutine(float speed, float time)
	{
		this.speed = speed;
		yield return new WaitForSeconds(time);
		this.speed = defaultSpeed;
		speedBuffCoroutine = null;
	}

	public void SpeedDeBuff(float speed, float time)
	{
		if (speedBuffCoroutine != null)
			return;

		if (speedDebuffCoroutine != null)
		{
			StopCoroutine(speedDebuffCoroutine);
		}

		speedDebuffCoroutine = StartCoroutine(SpeedDeBuffCoroutine(speed, time));
	}
	private IEnumerator SpeedDeBuffCoroutine(float speed, float time)
	{
		this.speed = speed;
		yield return new WaitForSeconds(time);
		this.speed = defaultSpeed;
		speedDebuffCoroutine = null;
	}

}
