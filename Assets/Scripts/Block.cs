using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Block : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;
    public Transform[] pocketTransforms;

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
        if (fullPockets >= 2)
            return;

        int obstacleIndex = Random.RandomRange(0, obstaclePrefabs.Length);
        int posIndex = 0;
        do
        {
            posIndex = Random.RandomRange(0, pockets.Length);
        } while (pockets[posIndex].go != null);

        pockets[posIndex].go = Instantiate(obstaclePrefabs[obstacleIndex], pocketTransforms[posIndex].position, Quaternion.identity);
        pockets[posIndex].go.transform.parent = transform;

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
        } while (pockets[posIndex].go != null);

        pockets[posIndex].go = Instantiate(itemPrefabs[itemIndex], pocketTransforms[posIndex].position, Quaternion.identity);
        pockets[posIndex].go.transform.parent = transform;  //Instantiate���� �θ� ��ü�� ���ع����� ������ ������ ����Ǳ� ������ ���� �� ����

        fullPockets++;
	}

    public void Clear()
    {
        fullPockets = 0;

        foreach (var pocket in pockets)
        {
            Destroy( pocket.go );
        }
    }

}
