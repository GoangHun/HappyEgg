using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Hen : Item
{
	public Collider rushCollider;
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
        ParentPocket.childGo = null;
		ParentPocket = null;
		transform.parent = ItemManager.Instance.conveyor.transform;
		animator.SetTrigger("isFly");
		StartCoroutine(RushCoroutine());
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
		Destroy(gameObject);
	}
}
