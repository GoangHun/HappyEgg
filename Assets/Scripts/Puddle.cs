using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : Obstacle
{
	public float time = 5f;
	public float speed = 9f;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<Player>();
			player.SpeedDeBuff(speed, time);
		}
	}
}
