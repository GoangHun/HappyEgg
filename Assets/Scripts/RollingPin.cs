using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RollingPin : ShootingItem
{
	public float time = 5f;
	public ParticleSystem particle;

	private Animator animator;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle") && other.name != "ShortBarrier(Clone)")
		{
			other.GetComponent<Obstacle>().OnSmash();
		}
	}

	private void Awake()
	{
		onAction = () =>
		{
			StartCoroutine(Rolling());
		};

		animator = GetComponent<Animator>();
	}

	private IEnumerator Rolling()
	{
		gameObject.SetActive(true);
		particle.Play();
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
