using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hen : Item
{
	public Collider rushCollider;
	public float durationTime = 3f;
	public float speed = 7f;

	protected bool isRush = false;
	
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		ParentPocket.childGo = null;
		ParentPocket = null;
		isRush = true;
		StartCoroutine(RushCoroutine());
	}

	private IEnumerator RushCoroutine()
	{
		rushCollider.enabled = true;
		while (true)
		{
			transform.position += transform.forward * speed * Time.deltaTime;
			yield return new WaitForSeconds(durationTime);
			break;
		}
		rushCollider.enabled = false;
		Destroy(gameObject);
	}
}
