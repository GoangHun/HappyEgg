using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
	public int score = 1000;
	public float speed = 10f;   //자석에 끌리는 속도
	public ParticleSystem pickupEffect;
	public bool IsMagnetic { get; set; } = false;

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
			var effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
			Destroy(effect, 1f);
		}
	}

    private void FixedUpdate()
    {
		if (IsMagnetic)
		{
			Move();
        }
    }

    public void PickUpItem()
	{
		GameManager.Instance.AddScore(score);
		ItemManager.Instance.ScoreItems.Remove(this);
		base.PickUpItem();
	}

    public void OnSmash()
    {
		ItemManager.Instance.ScoreItems.Remove(this);
		base.OnSmash();
    }

    public void Move()
	{
        var dir = GameManager.Instance.player.transform.position - transform.position;
        dir.Normalize();
		transform.position += dir * speed * Time.fixedDeltaTime;
	}

}
