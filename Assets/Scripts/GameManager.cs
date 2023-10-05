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

    public int Score { get; private set; } = 0;
    public float Timer { get; private set; } = 180f;
    public bool IsGameover { get; private set; }
    public bool IsPause { get; private set; }

	private void Awake()
	{
        if (instance != null)
        {
			Destroy(gameObject);
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
        }
    }

    public void EndGame()
    {
        IsGameover = true;
    }

}
