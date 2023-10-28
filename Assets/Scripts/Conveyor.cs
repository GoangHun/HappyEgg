using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM = GameManager;

public class Conveyor : MonoBehaviour
{
	public float speed = 0f;
	public Block lastBlock;
	public Player player;
    public List<Block> blocks = new List<Block>();

    private float defaultSpeed;
	private float blockHeight;
	private Coroutine speedBuffCoroutine;
	private Coroutine speedDebuffCoroutine; 


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

			OnTriggerState(target);
		}
	}

	void Update()
	{
		if (GM.Instance.IsGameover)
			return;
		foreach (var block in blocks)
		{
			block.transform.position += Vector3.back * speed * Time.deltaTime;
		}
	}

    private void OnTriggerState(Block target)
    {
        switch (GM.Instance.currentStage)
        {
            case Scene.ChallengeStage:
                // Challenge 상태에서의 업데이트 동작
                break;
            case Scene.SpecialStage:
				SpecialStageOnTrigger(target);
                break;
            case Scene.Stage1:
            case Scene.Stage2:
            case Scene.Stage3:
            case Scene.Stage4:
            case Scene.Stage5:
                NomalStageOnTrigger(target);
                break;
        }
    }

	private void NomalStageOnTrigger(Block target)
	{
        var pos = new Vector3(0, 0, blockHeight * blocks.Count);
        target.transform.position += pos;

        target.Clear();
        target.CreateItems();
        target.CreateTotalObstacles();
        target.CreateSocreItems();
    }

	private void SpecialStageOnTrigger(Block target)
	{
        if (SpawnManager.Instance.IsEnd)
        {
            if (!lastBlock.gameObject.activeSelf)
            {
                lastBlock.gameObject.SetActive(true);   //4번째 블럭 위치에 배치해 놓고 비활성화 시켜놓음
                blocks.Add(lastBlock);
            }
            //target.Clear();
            return;
        }

        var pos = new Vector3(0, 0, blockHeight * blocks.Count);
        target.transform.position += pos;

        target.Clear();
        target.SetObject();
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
