using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBarrier : Obstacle
{
	public float damage = 10f;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<Player>();
			player.OnDamage(damage);
		}
	}
}
