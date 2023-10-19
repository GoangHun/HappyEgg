using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Hen : Item
{
	public Collider rushCollider;
	public ParticleSystem rushEffect;
	public ParticleSystem destroyEffect;

	public float durationTime = 3f;
	public float speed = 7f;

	private Animator animator;
	private bool isRush;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		if (isRush)
			return;
        ParentPocket.ChildGo = null;
		ParentPocket = null;
		transform.parent = ItemManager.Instance.conveyor.transform;
		animator.SetTrigger("isFly");
		StartCoroutine(RushCoroutine());
		rushEffect.Play();
	}

	private IEnumerator RushCoroutine()
	{
		isRush = true;
		rushCollider.enabled = true;
		float startTime = Time.time;
		while (startTime + durationTime > Time.time)
		{
			transform.position += transform.forward * speed * Time.deltaTime;
			yield return null;
		}
		rushCollider.enabled = false;

		var effect = Instantiate(destroyEffect, transform.position + new Vector3(0, -5f, 0), Quaternion.identity);
		effect.Play();
		Destroy(effect, 3f);
		Destroy(gameObject);
	}
}
