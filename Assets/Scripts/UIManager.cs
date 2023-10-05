using UnityEngine;
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

}
