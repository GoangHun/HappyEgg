using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Item : MonoBehaviour, IItem
{
	public Pocket ParentPocket { get; private set; } = null;

	public void PickUpItem()
	{
		ParentPocket = null;
		Destroy(gameObject);
	}

	public void SetPocket(Pocket parentPocket)
	{
		transform.parent = parentPocket.transform;
		ParentPocket = parentPocket;
	}
}
