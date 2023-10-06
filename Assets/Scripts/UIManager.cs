using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
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
	public GameObject pausePanel;
	public GameObject countTextGO;
	public GameObject[] shootingItems;
	public float countTime = 3f;
	public float shootingTimer = 30f;

	private float lastShootingTime = 0f;
	private int currentShootingItem = 0;

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
		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			Pause();
		}
		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			Continue();
		}
		if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			Restart();
		}
	}

	public void UpdateTimerProgress(float time) 
	{
		timerProgress.fillAmount = time;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive(true);
	}

	public void Continue()
	{
		pausePanel.SetActive(false);
		StartCoroutine(Count());
	}

	private IEnumerator Count()
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
		Time.timeScale = 1f;
	}

}
