using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public float speed = 0f;

	public Spawner[] blocks;

	private float blockHeight;

	private void Awake()
	{
		blockHeight = blocks[0].transform.localScale.z;
	}

	public void OnTriggerExit(Collider collision)
	{
		var target = collision.GetComponent<Spawner>();
		if (collision.CompareTag("Block"))
		{
			var pos = new Vector3(0, 0, blockHeight * 2);
			target.transform.position += pos;
			target.Clear();
			target.CreateObstacle();
			target.CreateItem();
        }
	}

	void Update()
	{
		foreach (var block in blocks)
		{
			block.transform.position += Vector3.back * speed * Time.deltaTime;
		}
	}
}
