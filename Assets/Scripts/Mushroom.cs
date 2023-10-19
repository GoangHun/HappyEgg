using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Obstacle
{
	public float durationTime = 5f;
	public ParticleSystem destroyEffect;
	public AudioClip actionSound;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			var player = other.GetComponent<Player>();
			ObstacleManager.Instance.ActionMushroom(durationTime, player);
			AudioManager.instance.PlaySE(actionSound);
			destroyEffect.Play();
		}
	}
}
