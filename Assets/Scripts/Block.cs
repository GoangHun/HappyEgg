using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Block : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;
    public Transform[] childPoss;
    //오브젝트풀 사용 여부 보류
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private int installObject = 0;

    public void CreateObstacle()
    {
        if (installObject >= 2)
            return;

        int obstacleIndex = Random.RandomRange(0, obstaclePrefabs.Length);
        int posIndex = Random.RandomRange(0, childPoss.Length);

        Instantiate(obstaclePrefabs[obstacleIndex], childPoss[posIndex].position, Quaternion.identity);
		installObject++;
	}
	public void CreateItem()
    {
		if (installObject >= 2)
			return;

		int itemIndex = Random.RandomRange(0, itemPrefabs.Length);
		int posIndex = Random.RandomRange(0, childPoss.Length);

		Instantiate(itemPrefabs[itemIndex], childPoss[posIndex].position, Quaternion.identity);
		installObject++;
	}

}
