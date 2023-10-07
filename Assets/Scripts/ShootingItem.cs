using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : Item
{
	protected Action onAction;

	public void Action()
	{
		Debug.Log(onAction != null);
		if (onAction != null)
		{
            onAction();
        }		
    }
}
