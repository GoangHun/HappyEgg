using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;


public class Block : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private GameObject scoreItem;
    private GameObject[] itemPrefabs;
    private Dictionary<string, RezenInfo> itemRezenInfos;
	private List<string> keys;
	private Pocket[] pockets;
    private int isUsePocketCount = 0;

    //오브젝트풀 사용 여부 보류
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private void Awake()
    {
        pockets = GetComponentsInChildren<Pocket>();
    }

    private void Start()
    {
        scoreItem = ItemManager.Instance.scoreItemPrefab;
        itemPrefabs = ItemManager.Instance.itemPrefabs;
        itemRezenInfos = ItemManager.Instance.itemRezenInfos;
		keys = new List<string>(itemRezenInfos.Keys);
	}

	public void CreateObstacles()
    {
        GameManager.Instance.StageInfo.TryGetValue(GameManager.Instance.CurrentStageNum, out int num);
        for (int i = 0; i < num; i++)
        {
            if (isUsePocketCount >= pockets.Length)
                return;
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            int posIndex = Random.Range(0, pockets.Length);
            while (pockets[posIndex].childGo != null)
            {
                posIndex = Random.Range(0, pockets.Length);
            }

            var go = Instantiate(obstaclePrefabs[obstacleIndex], pockets[posIndex].transform.position, Quaternion.identity);
            var obstacle = go.GetComponent<Obstacle>();

            //Instantiate에서 부모 객체를 정해버리면 스케일 값까지 적용되기 때문에 생성 뒤 배정
            obstacle.SetPocket(pockets[posIndex]);
            ObstacleManager.Instance.Obstacles.Add(obstacle);
            pockets[posIndex].childGo = go;
            isUsePocketCount++;
        }    
	}

	public void CreateItems()
    {
		for (int i = 0; i < keys.Count; i++)
        {
			var key = keys[i];
			var rezenInfo = itemRezenInfos[key];

			if (rezenInfo.lastRezenTime + rezenInfo.rezenDuration < Time.time)
			{
				rezenInfo.lastRezenTime = Time.time;
				itemRezenInfos[key] = rezenInfo;

				int posIndex = Random.Range(0, pockets.Length);
				while (pockets[posIndex].childGo != null)
				{
					posIndex = Random.Range(0, pockets.Length);
				}

				switch (key)
				{
					case "Hourglass":
						Create(posIndex, 0);
						break;

					case "Magnet&Hen":
						int index = Random.Range(1, 3);
						Create(posIndex, index);
						break;

					default: break;
				}
			}
		}

        void Create(int posIndex, int index)
        {
			pockets[posIndex].childGo =
							Instantiate(itemPrefabs[index], pockets[posIndex].transform.position, Quaternion.identity);

			var item = pockets[posIndex].childGo.GetComponent<Item>();
			item.SetPocket(pockets[posIndex]);
			isUsePocketCount++;
		}
	}

    public void CreateSocreItems()
    {
        for (int i = 0; i < pockets.Length; i++)
        {
            if (isUsePocketCount >= pockets.Length)
                return;
            if (pockets[i].childGo != null)
                continue;

            pockets[i].childGo = Instantiate(scoreItem, pockets[i].transform.position, Quaternion.identity);

            var scoreItemComp = pockets[i].childGo.GetComponent<ScoreItem>();
            ItemManager.Instance.ScoreItems.Add(scoreItemComp);
            scoreItemComp.SetPocket(pockets[i]);
			scoreItemComp.IsMagnetic = ItemManager.Instance.IsMagnetic;
            isUsePocketCount++;
        }
    }

    public void Clear()
    {
        foreach (var pocket in pockets)
        {
            if (pocket.childGo == null)
                continue;

            if (pocket.childGo.CompareTag("Item"))
            {
                var item = pocket.childGo.GetComponent<Item>();
                item.OnSmash();
            }
            else if (pocket.childGo.CompareTag("Obstacle"))
            {
                var obstacle = pocket.childGo.GetComponent<Obstacle>();
                obstacle.OnSmash();
            }
        }
        isUsePocketCount = 0;
    }

}
