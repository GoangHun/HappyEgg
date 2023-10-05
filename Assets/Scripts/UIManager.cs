using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<UIManager>();
			}

			return instance;
		}
	}

	private static UIManager instance;

	public Image timerProgress;

	public void UpdateTimerProgress(float time) 
	{
		timerProgress.fillAmount = time;
	}

}
