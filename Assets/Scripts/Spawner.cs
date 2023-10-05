using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;

    private Pocket[] pockets;
    private int fullPockets = 0;

    //오브젝트풀 사용 여부 보류
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private void Awake()
    {
        pockets = GetComponentsInChildren<Pocket>();
    }

    public void CreateObstacle()
    {
        if (fullPockets >= 2)
            return;

        int obstacleIndex = Random.RandomRange(0, obstaclePrefabs.Length);
        int posIndex = 0;
        do
        {
            posIndex = Random.RandomRange(0, pockets.Length);
        } while (pockets[posIndex].childGo != null);

        pockets[posIndex].childGo = Instantiate(obstaclePrefabs[obstacleIndex], pockets[posIndex].transform.position, Quaternion.identity);
		pockets[posIndex].childGo.GetComponent<Obstacle>().SetPocket(pockets[posIndex]);    //Instantiate에서 부모 객체를 정해버리면 스케일 값까지 적용되기 때문에 생성 뒤 배정

		fullPockets++;
	}
	public void CreateItem()
    {
		if (fullPockets >= 2)
			return;

		int itemIndex = Random.RandomRange(0, itemPrefabs.Length);
        int posIndex = 0;
        do
        {
            posIndex = Random.RandomRange(0, pockets.Length);
        } while (pockets[posIndex].childGo != null);

        pockets[posIndex].childGo = Instantiate(itemPrefabs[itemIndex], pockets[posIndex].transform.position, Quaternion.identity);
        pockets[posIndex].childGo.GetComponent<Item>().SetPocket(pockets[posIndex]);

        fullPockets++;
	}

    public void Clear()
    {
        fullPockets = 0;

        foreach (var pocket in pockets)
        {
            if ( pocket.childGo != null)
            {
				Destroy(pocket.childGo);
				pocket.childGo = null;
			}
		}
    }
}
