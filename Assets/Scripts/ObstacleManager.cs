using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager instance;
    public static ObstacleManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void AllSmash()
    {
        while(Obstacles.Count > 0)
        {
            Obstacles[0].OnSmash();
        }
    }
}
