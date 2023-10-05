using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    public static ItemManager Instance
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
    [HideInInspector]public List<ScoreItem> scoreItems = new List<ScoreItem>();
}
