using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public struct RezenInfo
{
    public float rezenDuration;
    public float lastRezenTime;
}

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

    public Dictionary<string, RezenInfo> itemRezenInfos = new Dictionary<string, RezenInfo>();
    public Conveyor conveyor;
    public GameObject scoreItemPrefab;
    public GameObject[] itemPrefabs;    //리젠 우선 순위(1위, 2위...) 
	public GameObject[] shootingItems;
    public string[] rezenTableId;


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

        //데이터테이블에서 itemRezenInfos 읽어오기
        foreach (var id in rezenTableId)
        {
            var rezenDuration = DataTableMgr.GetTable<RezenTable>().GetRezenDuration(id);
            RezenInfo info = new RezenInfo { rezenDuration = rezenDuration, lastRezenTime = 0f };
            itemRezenInfos.Add(id, info);
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