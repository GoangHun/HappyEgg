using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Block : MonoBehaviour
{
    public Pocket[] pockets;

    private GameObject[] obstaclePrefabs;
    private GameObject mushroomPrefab;
    private GameObject[] scoreItems;
    private GameObject[] itemPrefabs;
    private Dictionary<string, RezenInfo> itemRezenInfos;
	private List<string> keys;
    private int isUsePocketCount = 0;
    private float mostFarDistance;

    //오브젝트풀 사용 여부 보류
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private void Start()
    {
        scoreItems = ItemManager.Instance.scoreItemPrefabs;
        itemPrefabs = ItemManager.Instance.itemPrefabs;
        itemRezenInfos = ItemManager.Instance.itemRezenInfos;
		obstaclePrefabs = ObstacleManager.Instance.obstaclePrefabs;
        mushroomPrefab = ObstacleManager.Instance.mushroomPrefab;

        keys = new List<string>(itemRezenInfos.Keys);
    }


    public void CreateTotalObstacles()
    {
        //GameManager.Instance.StageInfo.TryGetValue((int)GameManager.Instance.currentStage, out int num);
        if (ObstacleManager.Instance.barricadeCount > 0)
            CreateBarrier(ObstacleManager.Instance.barricadeCount);
		if (ObstacleManager.Instance.rockCount > 0)
            CreateObstacles(ObstacleManager.Instance.rockCount, obstaclePrefabs[0]);
        if (ObstacleManager.Instance.puddleCount > 0)
            CreateObstacles(ObstacleManager.Instance.puddleCount, obstaclePrefabs[1]);

        if (mushroomPrefab != null &&
			ObstacleManager.Instance.mushroomLastRezenTime + ObstacleManager.Instance.mushroomRezenDuration < Time.time)
        {
			CreateMushroom();
		}
			

		void CreateObstacles(int count, GameObject prefeb)
        {   
            for (int i = 0; i < count; i++)
            {
                int posIndex = Random.Range(0, pockets.Length);

                while (pockets[posIndex].ChildGo != null || !pockets[posIndex].Check(prefeb.tag))
                {
                    posIndex = Random.Range(0, pockets.Length);
                }

                var go = Instantiate(prefeb, pockets[posIndex].transform.position, Quaternion.identity);
                var obstacle = go.GetComponent<Obstacle>();

                obstacle.SetPocket(pockets[posIndex]);
                ObstacleManager.Instance.Obstacles.Add(obstacle);
                pockets[posIndex].ChildGo = go;
                isUsePocketCount++;
            }
        }

		void CreateBarrier(int count)
        {
			int posIndex = Random.Range(3, 6);

			while (pockets[posIndex].ChildGo != null)
			{
				posIndex = Random.Range(3, 6);
			}
			var go = Instantiate(obstaclePrefabs[2], pockets[posIndex].transform.position, Quaternion.identity);
			var obstacle = go.GetComponent<Obstacle>();

			obstacle.SetPocket(pockets[posIndex]);
			ObstacleManager.Instance.Obstacles.Add(obstacle);
			pockets[posIndex].ChildGo = go;
			isUsePocketCount++;
		}

        void CreateMushroom()
        {
			int posIndex = Random.Range(0, pockets.Length);

			while (pockets[posIndex].ChildGo != null)
			{
				posIndex = Random.Range(0, pockets.Length);
			}

			var go = Instantiate(mushroomPrefab, pockets[posIndex].transform.position, Quaternion.identity);
			var obstacle = go.GetComponent<Obstacle>();

			obstacle.SetPocket(pockets[posIndex]);
			ObstacleManager.Instance.Obstacles.Add(obstacle);
			pockets[posIndex].ChildGo = go;
			isUsePocketCount++;

            ObstacleManager.Instance.mushroomLastRezenTime = Time.time;
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
				while (pockets[posIndex].ChildGo != null)
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
			pockets[posIndex].ChildGo =
							Instantiate(itemPrefabs[index], pockets[posIndex].transform.position, Quaternion.identity);

			var item = pockets[posIndex].ChildGo.GetComponent<Item>();
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
            if (pockets[i].ChildGo != null)
                continue;

            pockets[i].ChildGo = Instantiate(scoreItems[0], pockets[i].transform.position, Quaternion.identity);

            var scoreItemComp = pockets[i].ChildGo.GetComponent<ScoreItem>();
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
            if (pocket.ChildGo == null)
                continue;

            if (pocket.ChildGo.CompareTag("Obstacle"))
            {
                var obstacle = pocket.ChildGo.GetComponent<Obstacle>();
                obstacle.OnSmash();
            }
            else
            {
                var item = pocket.ChildGo.GetComponent<Item>();
                item.OnSmash();
            }
        }
        isUsePocketCount = 0;
    }

    public void SetObject()
    {
        for (int i = 0; i < 3; i++)
        {
            var line = SpawnManager.Instance.ReadLine();
            var pref = new GameObject[3];
            pref[0] = ConvertToGameObject(line.Left);
            pref[1] = ConvertToGameObject(line.Middle);
            pref[2] = ConvertToGameObject(line.Right);

			for (int j = 0; j < 3; j++)
            {
                if (pref[j] == null)
                    continue;

				int index = i * 3 + j;
                var go = Instantiate(pref[j], pockets[index].transform.position, Quaternion.identity);
				pockets[index].ChildGo = go;

				if (go.CompareTag("Item"))
                {
                    var item = go.GetComponent<Item>();
					item.SetPocket(pockets[index]);
					isUsePocketCount++;

				}
                else if (pockets[index].ChildGo.CompareTag("Obstacle"))
                {
                    var obstacle = go.GetComponent<Obstacle>();
                    obstacle.SetPocket(pockets[index]);
                    ObstacleManager.Instance.Obstacles.Add(obstacle);
                    isUsePocketCount++;
				}
                else if (go.CompareTag("ScoreItem"))
                {
                    var scoreItemComp = go.GetComponent<ScoreItem>();
                    ItemManager.Instance.ScoreItems.Add(scoreItemComp);
                    scoreItemComp.SetPocket(pockets[index]);
                    scoreItemComp.IsMagnetic = ItemManager.Instance.IsMagnetic;
                    isUsePocketCount++;
				}
			}
		}
	}

    private GameObject ConvertToGameObject(int code)
    {
        int category = code / 10;
        int entity = code % 10;

		switch (category) 
        {
            case 0:
                return itemPrefabs[entity];
            case 1:
                return obstaclePrefabs[entity];
            case 2:
                return scoreItems[entity];
            default: return null;
        }   
    }

}
