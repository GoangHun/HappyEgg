using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SaveDataVC = SaveDataV1;

public enum SceneMode
{
    None = -1,
    Title,
    Main,
}

public class TitleManager : MonoBehaviour
{
    public GameObject fpsUI;
    public GameObject titlePanel;
    public GameObject mainPanel;
    public GameObject optionPanel;

	public Button stage2Button;
	public Button stage3Button;
	public Button stage4Button;
	public Button stage5Button;

    public GameObject stage2Rock;
	public GameObject stage3Rock;
	public GameObject stage4Rock;
	public GameObject stage5Rock;

	public AudioClip titleBgm;
    public AudioClip titleButtonSound;
    public AudioClip mainBgm;

    public FadeInOut fadeInOut;

    private static TitleManager instance;
    private ISceneState currentState;

    private TitleState title = new TitleState();
    private MainState main = new MainState();

    public static TitleManager Instance
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
    }

    private void Start()
    {
        var saveData = SaveLoadSystem.Load("saveData.json") as SaveDataVC;
        if (saveData != null)
        {
            stage2Button.enabled = saveData.ClearStage2;
            stage2Rock.SetActive(!saveData.ClearStage2);
            stage3Button.enabled = saveData.ClearStage3;
            stage3Rock.SetActive(!saveData.ClearStage3);
            stage4Button.enabled = saveData.ClearStage4;
            stage4Rock.SetActive(!saveData.ClearStage4);
            stage5Button.enabled = saveData.ClearStage5;
            stage5Rock.SetActive(!saveData.ClearStage5);
        }

		currentState = title;
        AudioManager.instance.PlayBGM(titleBgm);
        Time.timeScale = 1.0f;
	}


    public void ChangeState(SceneMode mode)
    {
        currentState?.Exit(); // 현재 상태에서 나갈 때 실행할 작업

        switch (mode)
        {
            case SceneMode.Title:
                currentState = title;
                break;
            case SceneMode.Main:
                currentState = main;
                break;
            default:
                currentState = null;
                break;
        }

        currentState?.Enter(); // 새로운 상태에 진입할 때 실행할 작업
    }

    public void Update()
    {
        currentState?.Execute(); // 현재 상태에서 수행할 작업


#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha9) || (Input.touchCount == 3 && Input.GetTouch(2).phase == TouchPhase.Began))
            fpsUI.SetActive(!fpsUI.activeSelf);
#elif UNITY_ANDROID
        if (Input.touchCount == 3 && Input.GetTouch(2).phase == TouchPhase.Began)
            fpsUI.SetActive(!fpsUI.activeSelf);
#endif

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

    public void SetResolution()
    {
        int setWidth = 768;
        int setHeight = 1366;

        Screen.SetResolution(setWidth, setHeight, false);
    }
}

public interface ISceneState
{
    void Enter();
    void Execute();
    void Exit();
}

public class TitleState : ISceneState
{
    public void Enter()
    {
        TitleManager.Instance.titlePanel.SetActive(true);
		AudioManager.instance.PlayBGM(TitleManager.Instance.titleBgm);
	}

    public void Execute()
    {
        if (Input.anyKeyDown)
        {
            TitleManager.Instance.ChangeState(SceneMode.Main);
			AudioManager.instance.PlaySE(TitleManager.Instance.titleButtonSound);
		}

		//Press Ani Key 효과
	}

    public void Exit()
    {
        TitleManager.Instance.titlePanel.SetActive(false);
    }

    
}

public class MainState : ISceneState
{
    public void Enter()
    {
        TitleManager.Instance.mainPanel.SetActive(true);
		AudioManager.instance.PlayBGM(TitleManager.Instance.mainBgm);
	}

	public void Execute()
    {
    }

    public void Exit()
    {
        TitleManager.Instance.mainPanel.SetActive(false);
    }
}


