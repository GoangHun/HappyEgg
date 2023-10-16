using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Broom : ShootingItem
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
			StartCoroutine(Sweep());
		};

		animator = GetComponent<Animator>();
	}

	private IEnumerator Sweep()
	{
		gameObject.SetActive(true);
		particle.gameObject.SetActive(true);
		particle.Play();
		yield return new WaitForSeconds(time);
		particle.Stop();
		particle.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
}
