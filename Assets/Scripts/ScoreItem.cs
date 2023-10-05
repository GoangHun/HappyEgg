using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
	public int score = 1000;
	public float speed = 1f;	//자석에 끌리는 속도
	[HideInInspector]public bool isMagnetic = false;


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
		ItemManager.Instance.scoreItems.Remove(this);
		base.PickUpItem();
	}

	public void Move(Vector3 dir)
	{
		dir.Normalize();
		transform.position = dir * speed * Time.deltaTime;
	}

}
