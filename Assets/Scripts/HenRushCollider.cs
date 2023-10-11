using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushCollider : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle"))
		{
			other.GetComponent<Obstacle>().ChangeToSocreItem();
			//보석생성
		}
	}
}
