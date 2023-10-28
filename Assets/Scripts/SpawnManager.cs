using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public int Header { get; private set; } = 1;
	public bool IsEnd { get; private set; }

    private static SpawnManager instance;

    public static SpawnManager Instance
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

	public LineInfo ReadLine()	//블럭 루프 때 호출
	{
		LineInfo lineInfo = new LineInfo();
        lineInfo = DataTableMgr.GetTable<StageTable>(TableType.SP1).GetLine(Header++);
        IsEnd = Header >= DataTableMgr.GetTable<StageTable>(TableType.SP1).Count();

		return lineInfo;
	}
}
