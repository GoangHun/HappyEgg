using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
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

    public int Score { get; private set; } = 0;
    public float Timer { get; private set; } = 180f;
    public bool IsGameover { get; private set; }
    public bool IsPause { get; private set; }
    public Player Player { get; set; }

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

	private void Update()
	{
		if (!IsGameover)
        {
            Timer -= Time.deltaTime;
            UIManager.Instance.UpdateTimerProgress(Timer / 180f);

            if (Timer < 0)
            {
                EndGame();
            }
        }
	}

	public void AddScore(int score)
    {
		if (!IsGameover)
		{
			Score += score;
            Debug.Log("Score: " + Score);
		}
	}

	public void SetTimer(float time)
    {
        if (!IsGameover)
        {
            Timer += time;
            Timer = Timer > 180f ? 180f : Timer;
        }
    }

    public void EndGame()
    {
        IsGameover = true;
    }

}
