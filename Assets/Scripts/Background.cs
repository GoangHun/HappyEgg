using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public float speed = 0f;

	public Transform[] blocks;

	private float blockHeight;

	private void Awake()
	{
		blockHeight = blocks[0].localScale.z;
	}

	public void OnTriggerExit(Collider collision)
	{
		var target = collision.transform;
		if (collision.CompareTag("Block"))
		{
			var pos = new Vector3(0, 0, blockHeight * 2);
			target.position += pos;
		}
		else if (collision.CompareTag("Obstacle"))
		{
			//float randomX = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
			//float randomY = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
			//Vector2 randomPoint = new Vector2(randomX, randomY);
			//target.position = randomPoint;
		}
	}

	void Update()
	{
		foreach (var block in blocks)
		{
			block.position += Vector3.back * speed * Time.deltaTime;
		}
	}
}
