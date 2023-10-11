using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
	public int score = 1000;
	public float speed = 10f;	//자석에 끌리는 속도
	public bool IsMagnetic { get; set; } = false;

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PickUpItem();
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
        if (transform.parent != ItemManager.Instance.conveyor.transform)
            transform.parent = ItemManager.Instance.conveyor.transform;

        var dir = GameManager.Instance.Player.transform.position - transform.position;
        dir.Normalize();
		transform.position += dir * speed * Time.fixedDeltaTime;
	}

}
