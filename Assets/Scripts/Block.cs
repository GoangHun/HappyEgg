using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Block : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;
    public Transform[] childPoss;
    //������ƮǮ ��� ���� ����
    //public ObjectPool<GameObject> obstaclePool;
    //public ObjectPool<GameObject> itemPool;

    private int obstacleCount = 0;

	private void Awake()
	{

	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObstacle()
    {
        if (obstacleCount >= 2)
            return;

        int obstacleIndex = Random.RandomRange(0, obstaclePrefabs.Length);
        int posIndex = Random.RandomRange(0, childPoss.Length);

        Instantiate(obstaclePrefabs[obstacleIndex], childPoss[posIndex].position, Quaternion.identity);
        obstacleCount++;
	}
	public void CreateItem()
    {

    }

}
