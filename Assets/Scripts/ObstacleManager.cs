using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
	public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>();
	public GameObject[] obstaclePrefabs;
    public GameObject mushroomPrefab;

	public int rockCount;
    public int puddleCount;
    public int barricadeCount;

    public float mushroomRezenDuration;
    [HideInInspector]public float mushroomLastRezenTime = 0f;

	public bool IsMushroom { get; set; } = false;

	private static ObstacleManager instance;
    public static ObstacleManager Instance
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

    public void LastRezenTimeUpdate()
    {
        mushroomLastRezenTime = Time.time;
    }

	public void ActionMushroom(float time, Player player)
	{
		StartCoroutine(MushroomCoroutine(time, player));
	}

	public IEnumerator MushroomCoroutine(float time, Player player)
	{
		IsMushroom = true;
        player.confusionEffect.gameObject.SetActive(true);
        player.confusionEffect.Play();
		yield return new WaitForSeconds(time);
		player.confusionEffect.Stop();
        player.confusionEffect.gameObject.SetActive(false);
        IsMushroom = false;
	}

	public void AllSmash()  
    {
		for (int i = Obstacles.Count - 1; i >= 0; i--)
		{
			if (Obstacles[i].name == "ShortBarrier(Clone)")
				continue;
			Obstacles[i].OnSmash();	
		}
    }
}
