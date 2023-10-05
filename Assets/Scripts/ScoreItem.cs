using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
	public int score = 10;


	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
		}
	}

	public void PickUpItem()
	{
		GameManager.Instance.AddScore(score);
		base.PickUpItem();
	}

}
