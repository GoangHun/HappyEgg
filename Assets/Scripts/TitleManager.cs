using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneMode
{
    None = -1,
    Title,
    Main,
    Option,
}

public class TitleManager : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject mainPanel;
    public GameObject optionPanel;

    private static TitleManager instance;
    private ISceneState currentState;

    private TitleState title = new TitleState();
    private MainState main = new MainState();
    private OptionState option = new OptionState();

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
        //test code
        currentState = main;
        //SetResolution();
    }


    public void ChangeState(SceneMode mode)
    {
        currentState?.Exit(); // ���� ���¿��� ���� �� ������ �۾�

        switch (mode)
        {
            case SceneMode.Title:
                currentState = title;
                break;
            case SceneMode.Main:
                currentState = main;
                break;
            case SceneMode.Option:
                currentState = option;
                break;
            default:
                currentState = null;
                break;
        }

        currentState?.Enter(); // ���ο� ���¿� ������ �� ������ �۾�
    }

    public void Update()
    {
        currentState?.Execute(); // ���� ���¿��� ������ �۾�
    }

    public void SceneLoad(int num)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Stage1");
            break;
            case 1:
                SceneManager.LoadScene("Stage2");
            break;
            case 2:
                SceneManager.LoadScene("Stage3");
            break;
            case 3:
                SceneManager.LoadScene("Stage4");
            break;
            case 4:
                SceneManager.LoadScene("Stage5");
            break;
            case 5:
                SceneManager.LoadScene("ChallengeStage");
            break;  
            case 6:
                SceneManager.LoadScene("SpecialStage");
            break;   
        }
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
    }

    public void Execute()
    {
        if (Input.anyKeyDown)
        {
            TitleManager.Instance.ChangeState(SceneMode.Main);
        }

        //Press Ani Key ȿ��
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
    }

    public void Execute()
    {
        // Main ���¿��� ������ �۾�
    }

    public void Exit()
    {
        TitleManager.Instance.mainPanel.SetActive(false);
    }
}

public class OptionState : ISceneState
{
    public void Enter()
    {
        TitleManager.Instance.optionPanel.SetActive(true);
    }

    public void Execute()
    {
        // Option ���¿��� ������ �۾�
    }

    public void Exit()
    {
        TitleManager.Instance.optionPanel.SetActive(false);
    }
}

