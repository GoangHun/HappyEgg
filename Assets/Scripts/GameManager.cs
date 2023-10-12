using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    public Player player;
    public float playTime = 180f;
    private float playTimer;
    public int Score { get; private set; } = 0;
    public bool IsGameover { get; private set; }
    public bool IsPause { get; private set; }
    public Dictionary<int, int> StageInfo { get; private set; } = new Dictionary<int, int>(); //스테이지 넘버, 장애물 생성 개수
    public int CurrentStageNum { get; set; } = 1;

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

        playTimer = playTime;
    }

	private void Update()
	{
		if (!IsGameover)
        {
            playTimer -= Time.deltaTime;
            UIManager.Instance.UpdateTimerProgress(playTimer / playTime);

            if (playTimer < 0)
            {
                EndGame();
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                UIManager.Instance.Restart();
            }
        }
	}

	public void AddScore(int score)
    {
		if (!IsGameover)
		{
			Score += score;
		}
	}

	public void SetTimer(float time)
    {
        if (!IsGameover)
        {
            playTimer += time;
            playTimer = playTimer > playTime ? playTime : playTimer;
        }
    }

    public void Play()
    {
        IsPause = false;
        Time.timeScale = 1f;
    }
	public void Pause()
	{
        IsPause = true;
		Time.timeScale = 0f;
	}

	public void EndGame()
    {
        IsGameover = true;
        UIManager.Instance.scoreTextGO.SetActive(true);
        Time.timeScale = 0f;
    }

}
