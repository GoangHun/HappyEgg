using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyHammer : ShootingItem
{
private void Awake()
    {
        onAction = () =>
        {
            gameObject.SetActive(true);
            ObstacleManager.Instance.AllSmash();
            gameObject.SetActive(false);
        };
        gameObject.SetActive(false);
    }
}
