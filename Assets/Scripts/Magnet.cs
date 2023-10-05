using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
	public float time = 8f;
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		StartCoroutine(Action());
		base.PickUpItem();
	}

	private IEnumerator Action()
	{
		GameManager.Instance.Player.isMagnetic = true;
		yield return new WaitForSeconds(time);
		GameManager.Instance.Player.isMagnetic = false;
	}
}
