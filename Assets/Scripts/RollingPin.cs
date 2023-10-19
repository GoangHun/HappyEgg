using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RollingPin : ShootingItem
{
	public float time = 3f;
	public ParticleSystem particle;

	private Animator animator;

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "RockObstacle(Clone)")
		{
			other.GetComponent<Rock>().OnSmash();
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
