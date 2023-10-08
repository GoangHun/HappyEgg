using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

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

    public Dictionary<int, int> StageInfo { get; private set; } = new Dictionary<int, int>(); //스테이지 넘버, 장애물 생성 개수
    public int CurrentStageNum { get; private set; } = 1;

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

        var obstacleTable = DataTableMgr.GetTable<ObstacleTable>();
        for (int i = 1; i <= obstacleTable.GetCount(); i++)
        {
            StageInfo.Add(i, obstacleTable.GetObstacleCount(i));
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
