using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Obstacle
{
	public float damage = 10f;
	public GameObject destoryEffect;
	public AudioClip destroySound;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<Player>();
			player.OnDamage(damage);
			Camera.main.GetComponent<StressReceiver>().InduceStress(0.5f);
			
		}
	}

	public void OnSmash()
	{
		var effect = Instantiate(destoryEffect, transform.position, Quaternion.identity);
		AudioManager.instance.PlaySE(destroySound);
		Destroy(effect, 1f);
		base.OnSmash();
	}
}
