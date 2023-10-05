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

    public bool IsMagnetic { get; set; } = false;
    public List<ScoreItem> ScoreItems { get; set; } = new List<ScoreItem>();

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

    public void ToMagnetic()
    {
        foreach (var item in ScoreItems)
        {
            item.IsMagnetic = true;
        }
    }

    public void ActionMagnet(float time)
    {
        StartCoroutine(MagnetCoroutine(time));
    }

    public IEnumerator MagnetCoroutine(float time)
    {
        IsMagnetic = true;
        ToMagnetic();
        yield return new WaitForSeconds(time);
        IsMagnetic = false;
    }
}
