using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Item : MonoBehaviour, IItem
{
	public Pocket ParentPocket { get; protected set; } = null;


    public void PickUpItem()
	{
		ParentPocket.ChildGo = null;
		ParentPocket = null;
		Destroy(gameObject);
	}

	public void SetPocket(Pocket parentPocket)
	{
		transform.parent = parentPocket.transform;
		ParentPocket = parentPocket;
	}

    public void OnSmash()
    {
        ParentPocket.ChildGo = null;
        ParentPocket = null;
        Destroy(gameObject);
    }
}
