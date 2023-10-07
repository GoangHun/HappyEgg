using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class Block : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;

    private Pocket[] pockets;

    //오브젝트풀 사용 여부 보류
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private void Awake()
    {
        pockets = GetComponentsInChildren<Pocket>();
    }

    public void CreateObstacle()
    {
        int obstacleIndex = Random.RandomRange(0, obstaclePrefabs.Length);
        int posIndex = 0;
        do
        {
            posIndex = Random.RandomRange(0, pockets.Length);
        } while (pockets[posIndex].childGo != null);

        var go = Instantiate(obstaclePrefabs[obstacleIndex], pockets[posIndex].transform.position, Quaternion.identity);
        var obstacle = go.GetComponent<Obstacle>();
        //Instantiate에서 부모 객체를 정해버리면 스케일 값까지 적용되기 때문에 생성 뒤 배정
        obstacle.SetPocket(pockets[posIndex]);
        ObstacleManager.Instance.Obstacles.Add(obstacle);
        pockets[posIndex].childGo = go;
	}

	public void CreateItem()
    {
		int itemIndex = Random.RandomRange(0, itemPrefabs.Length);
        int posIndex = 0;
        do
        {
            posIndex = Random.RandomRange(0, pockets.Length);
        } while (pockets[posIndex].childGo != null);

        pockets[posIndex].childGo = Instantiate(itemPrefabs[itemIndex], pockets[posIndex].transform.position, Quaternion.identity);

        if (itemIndex == 0)
        {
			var item = pockets[posIndex].childGo.GetComponent<ScoreItem>();
			ItemManager.Instance.ScoreItems.Add(item);
			item.SetPocket(pockets[posIndex]);
            item.IsMagnetic = ItemManager.Instance.IsMagnetic;
		}
		else
        {
			var item = pockets[posIndex].childGo.GetComponent<Item>();
			item.SetPocket(pockets[posIndex]);
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
    }
}
