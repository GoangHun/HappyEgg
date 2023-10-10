using System.Collections;
using TMPro;
using UnityEngine.SearchService;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

	public Image timerProgress;
	public GameObject scoreTextGO;
	public GameObject pausePanel;
	public GameObject countTextGO;
	public float countTime = 3f;
	public float shootingTimer = 30f;


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
		timerProgress.fillAmount = time;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
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
