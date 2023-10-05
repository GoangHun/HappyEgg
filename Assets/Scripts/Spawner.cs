using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;

    private Pocket[] pockets;
    private int fullPockets = 0;

    //������ƮǮ ��� ���� ����
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

        pockets[posIndex].childGo = Instantiate(obstaclePrefabs[obstacleIndex], pockets[posIndex].transform.position, Quaternion.identity);
		//Instantiate���� �θ� ��ü�� ���ع����� ������ ������ ����Ǳ� ������ ���� �� ����
		pockets[posIndex].childGo.GetComponent<Obstacle>().SetPocket(pockets[posIndex]);
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
			ItemManager.Instance.scoreItems.Add(item);
			item.SetPocket(pockets[posIndex]);
		}
		else
        {
			var item = pockets[posIndex].childGo.GetComponent<Item>();
			item.SetPocket(pockets[posIndex]);
		}
	}

    public void Clear()
    {
        fullPockets = 0;

        foreach (var pocket in pockets)
        {
            if ( pocket.childGo != null)
            {
                var scoreItem = pocket.childGo.GetComponent<ScoreItem>();
				if (scoreItem != null)
					ItemManager.Instance.scoreItems.Remove(scoreItem);

				Destroy(pocket.childGo);
				pocket.childGo = null;
			}
		}
    }
}
