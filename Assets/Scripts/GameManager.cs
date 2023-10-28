using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using SaveDataVC = SaveDataV1;
using UnityEngine.UI;

public enum Scene
{
    None = -1,
    Stage1 = 1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    ChallengeStage,
    SpecialStage,
    TitleScene,
}

public class GameManager : MonoBehaviour
{
	public Player player;
	public float playTime = 180f;

    public FadeInOut fadeInOut;

	private float playTimer;
	public Scene currentStage = Scene.None;
	public int Score { get; private set; } = 0;
	public int RandomStageLevel { get; set; } = 1;  //도전 모드에서 사용할 가변 난이도 레벨에 사용
	public bool IsGameover { get; private set; } = true;
    public bool IsPause { get; private set; } = false;
	//public Dictionary<int, int> StageInfo { get; private set; } = new Dictionary<int, int>(); //스테이지 넘버, 장애물 생성 개수

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
            case Scene.ChallengeStage:
                // Challenge 상태에서의 업데이트 동작
                break;
            case Scene.SpecialStage:
                SpecialSgateUpdate();
                break;
            case Scene.Stage1:
            case Scene.Stage2:
            case Scene.Stage3:
            case Scene.Stage4:
            case Scene.Stage5:
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

    public bool CheckStageCompleted()
    {
		return Score >= stageInfo.clearScore;
    }

    public void Save()
    {
		var saveData = new SaveDataVC();
		var loadData = SaveLoadSystem.Load("saveData.json") as SaveDataVC;
		if (loadData != null)
		{
            saveData = loadData;
			saveData.BestScore = Score > loadData.BestScore ? Score : loadData.BestScore;
		}
		else
		{
			saveData.BestScore = Score;
		}

        if ((int)currentStage <= 4)
            saveData.GetType().GetProperty("ClearStage" + ((int)currentStage + 1)).SetValue(saveData, true);
		SaveLoadSystem.Save(saveData, "saveData.json");
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
        AudioManager.instance.StopBGM();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR    //유니티 에디터에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else   //빌드된 에플리케이션 종료
    Application.Quit();     
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
	}

    public void StartLoadScene(string name)
    {
        fadeInOut.StartFadeOut();
        StartCoroutine(LoadScene(name));
    }

    public IEnumerator LoadScene(string name)
    {
        while (fadeInOut.isFading)
            yield return null;
       
        SceneManager.LoadScene(name);
    }
}
