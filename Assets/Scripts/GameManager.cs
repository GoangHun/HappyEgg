using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private static GameManager instance;

    public float Timer { get; private set; }
    public bool IsGameover { get; private set; }
    public bool IsPause { get; private set; }

	private void Awake()
	{
        if (instance != null)
        {
			Destroy(gameObject);
		}
	}

    public void AddTime(float time)
    {
        if (!IsGameover)
        {
            Timer += time;
        }
    }

    public void EndGame()
    {
        IsGameover = true;
    }

}
