using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
	public float speed = 0f;

	public Block[] blocks;

	private float blockHeight;

	private void Awake()
	{
		blockHeight = blocks[0].transform.localScale.z;
	}

	public void OnTriggerExit(Collider collision)
	{
		if (collision.CompareTag("Block"))
		{
			var target = collision.GetComponent<Block>();
			var pos = new Vector3(0, 0, blockHeight * blocks.Length);
			target.transform.position += pos;
			//Debug.Log("블럭 이동", collision.gameObject);
			target.Clear();
            target.CreateItems();
            target.CreateObstacles();
			target.CreateSocreItems();
        }
	}

	void Update()
	{
		foreach (var block in blocks)
		{
			block.transform.position += Vector3.back * speed * Time.deltaTime;
		}
	}

	public void SpeedUpBuff(float speed, float time)
	{
		StartCoroutine(SpeedUpCoroutine(speed, time));
	}

	private IEnumerator SpeedUpCoroutine(float speed, float time)
	{
		this.speed = speed;
		yield return new WaitForSeconds(time);
		this.speed = 12f;
	}


}
