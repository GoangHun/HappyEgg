using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : Item
{
	public event Action onAction;

	public void Action()
	{
		if (onAction != null) onAction();
	}
}
