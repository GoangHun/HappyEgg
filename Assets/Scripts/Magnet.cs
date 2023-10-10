using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
	public float durationTime = 8f;
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		ItemManager.Instance.ActionMagnet(durationTime);
        base.PickUpItem();
    }
}