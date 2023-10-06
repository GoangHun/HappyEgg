using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : Item
{
	

	public void SetPocket(Pocket playerPocket)
	{
		transform.parent = playerPocket.transform;
		ParentPocket = playerPocket;
	}

}
