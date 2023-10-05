using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(player.OnHitEffect());
			player.OnDamage(damage);
		}
    }

    public void OnDamage()
    {
		ParentPocket.childGo = null;
		ParentPocket = null;
		Destroy(gameObject);
	}

	public void SetPocket(Pocket parentPocket)
    {
		transform.parent = parentPocket.transform;
		ParentPocket = parentPocket;
	}

}
