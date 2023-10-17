using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public enum Stage
{
    None = -1,
    One = 1,
    Two,
    Three,
    Four,
    Five,
    Challenge,
    Special,
}

public class GameManager : MonoBehaviour
{
	public Player player;
	public float playTime = 180f;
	private float playTimer;

	public Stage currentStage = Stage.None;
	public int Score { get; private set; } = 0;
	public int RandomStageLevel { get; set; } = 1;  //���� ��忡�� ����� ���� ���̵� ����
	public bool IsGameover { get; private set; } = false;
	public bool IsPause { get; private set; }
	//public Dictionary<int, int> StageInfo { get; private set; } = new Dictionary<int, int>(); //�������� �ѹ�, ��ֹ� ���� ����

	private StageInfo stageInfo;
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

		stageInfo = GetComponent<StageInfo>();

		/*var obstacleTable = DataTableMgr.GetTable<ObstacleTable>(TableType.ObstacleTable);
        for (int i = 1; i <= obstacleTable.GetCount(); i++)
        {
            StageInfo.Add(i, obstacleTable.GetObstacleCount(i));
        }*/
    }

    private void Start()
    {
        playTimer = playTime;
        Pause();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (currentStage)
        {
            case Stage.Challenge:
                // Challenge ���¿����� ������Ʈ ����
                break;
            case Stage.Special:
                SpecialSgateUpdate();
                break;
            case Stage.One:
            case Stage.Two:
            case Stage.Three:
            case Stage.Four:
            case Stage.Five:
                NomalStageUpdate();
                break;
        }
    }

    private void NomalStageUpdate()
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
    }

    private void SpecialSgateUpdate()
    {
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

    public bool CheckStageComplet()
    {
        return Score >= stageInfo.clearScore;
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

    public void StartGame()
    {
        IsGameover = false;
        ItemManager.Instance.LastRezenTimerUpdate();
        ObstacleManager.Instance.LastRezenTimeUpdate();
        Play();
    }

    public void EndGame()
    {
        IsGameover = true;
        Pause();
        UIManager.Instance.EndGame();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    //����Ƽ �����Ϳ��� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else   //����� ���ø����̼� ����
    Application.Quit();     
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
	}

    public void SceneLoad(int num)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Stage1");
                currentStage = Stage.One;
                break;
            case 1:
                SceneManager.LoadScene("Stage2");
                currentStage = Stage.Two;
                break;
            case 2:
                SceneManager.LoadScene("Stage3");
                currentStage = Stage.Three;
                break;
            case 3:
                SceneManager.LoadScene("Stage4");
                currentStage = Stage.Four;
                break;
            case 4:
                SceneManager.LoadScene("Stage5");
                currentStage = Stage.Five;
                break;
            case 5:
                SceneManager.LoadScene("ChallengeStage");
                currentStage = Stage.Challenge;
                break;
            case 6:
                SceneManager.LoadScene("SpecialStage");
                currentStage = Stage.Special;
                break;
            default:
				//LoadScene�� ���� �����ӿ� �����ϱ� ������ ���� �ڵ�� ��� ������.
				SceneManager.LoadScene("TitleScene");   
                currentStage = Stage.None;
				IsGameover = true;
				Play();
                break;
        }
    }
}
