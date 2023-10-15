using System.Collections;
using TMPro;
using UnityEngine.SearchService;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject timerProgress;
    public GameObject pauseButton;
    public GameObject gameEndUI;
    public GameObject pausePanel;
    public GameObject countTextGO;
    public Image timerProgressImage;

    public float countTime = 3f;
    
    private static UIManager instance;
	public static UIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = null;
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

    public void UpdateTimerProgress(float time) 
	{
        timerProgressImage.fillAmount = time;
	}

	public void EndGame()
	{
        gameEndUI.SetActive(true);
        pauseButton.SetActive(false);
		if (timerProgress != null)
			timerProgress.SetActive(false);
    }



	public void Pause()
	{
		if (!GameManager.Instance.IsPause)
		{
			GameManager.Instance.Pause();
			pausePanel.SetActive(true);
		}	
	}

	public void Continue()
	{
		pausePanel.SetActive(false);
		StartCoroutine(ContinueCoroutine());
	}

	private IEnumerator ContinueCoroutine()
	{
		countTextGO.SetActive(true);
		var countText = countTextGO.GetComponent<TextMeshProUGUI>();
		int timer = (int)countTime;
		while (timer > 0) 
		{
			yield return new WaitForSecondsRealtime(1f);
			countText.text = (--timer).ToString();
		}
		countTextGO.SetActive(false);
		countText.text = countTime.ToString();
		GameManager.Instance.Play();
	}

}
