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

	public void ActionMushroom(float time, Player player)
	{
		StartCoroutine(MushroomCoroutine(time, player));
	}

	public IEnumerator MushroomCoroutine(float time, Player player)
	{
		IsMushroom = true;
        player.confusionEffect.Play();
		Debug.Log("파티클1" + player.confusionEffect.isPlaying);   //트루
		yield return new WaitForSeconds(time);
		Debug.Log("파티클2" + player.confusionEffect.isPlaying);   //펄스
		player.confusionEffect.Stop();
		Debug.Log("파티클3" + player.confusionEffect.isPlaying);   //펄스
		IsMushroom = false;
	}

	public void AllSmash()  
    {
        for (int i = 0; i < Obstacles.Count; i++)
        {
			if (Obstacles[i].name == "ShortBarrier(Clone)")
				continue;
			Obstacles[i].OnSmash();
		}
    }
}
