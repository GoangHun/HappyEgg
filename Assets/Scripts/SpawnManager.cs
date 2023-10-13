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
		switch (GameManager.Instance.currentStage)
		{
			case Stage.One:
				lineInfo = DataTableMgr.GetTable<StageTable>(TableType.Stage1).GetLine(Header++);
			break;

			case Stage.Two:
				lineInfo = DataTableMgr.GetTable<StageTable>(TableType.Stage2).GetLine(Header++);
			break;

			case Stage.Three:
				lineInfo = DataTableMgr.GetTable<StageTable>(TableType.Stage3).GetLine(Header++);
			break;

			case Stage.Four:
				lineInfo = DataTableMgr.GetTable<StageTable>(TableType.Stage4).GetLine(Header++);
			break;

			case Stage.Five:
				lineInfo = DataTableMgr.GetTable<StageTable>(TableType.Stage5).GetLine(Header++);
			break;
		}

		IsEnd = Header >= DataTableMgr.GetTable<StageTable>(TableType.Stage1).Count();

		return lineInfo;
	}

	

}
