using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float damage = 10f;
	public Pocket ParentPocket { get; private set; } = null;

	public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			var player = other.GetComponent<Player>();
            player.OnDamage(damage);
        }
    }

    public void OnSmash()
    {
        ObstacleManager.Instance.Obstacles.Remove(this);
        ParentPocket.ChildGo = null;
		ParentPocket = null;
		Destroy(gameObject);
	}

	public void SetPocket(Pocket parentPocket)
    {
		transform.parent = parentPocket.transform;
		ParentPocket = parentPocket;
	}

	public void ChangeToSocreItem()
	{
		ParentPocket.ChildGo = Instantiate(ItemManager.Instance.scoreItemPrefab, ParentPocket.transform.position, Quaternion.identity);

		var scoreItemComp = ParentPocket.ChildGo.GetComponent<ScoreItem>();
		ItemManager.Instance.ScoreItems.Add(scoreItemComp);
		scoreItemComp.SetPocket(ParentPocket);
		scoreItemComp.IsMagnetic = ItemManager.Instance.IsMagnetic;

		ObstacleManager.Instance.Obstacles.Remove(this);
		ParentPocket = null;
		Destroy(gameObject);
	}

}
