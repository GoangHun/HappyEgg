using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : Item
{
	public float time = 20f;
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		GameManager.Instance.SetTimer(time);
		base.PickUpItem();
	}

}
