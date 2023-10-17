using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableType
{
    ObstacleTable = 0,
    RezenTable,
    Stage1, //Special Stage1
}

public static class DataTableMgr
{
    //Key로 System.Type을 사용
    private static Dictionary<TableType, DataTable> tables = new Dictionary<TableType, DataTable>();

    static DataTableMgr()
    {
        tables.Clear();

		/*var obstacleTable = new ObstacleTable();
		tables.Add(TableType.ObstacleTable, obstacleTable);*/
		var rezenTable = new RezenTable();
        tables.Add(TableType.RezenTable, rezenTable);
        var stageTable = new StageTable("1");
        tables.Add(TableType.Stage1, stageTable);
       
    }

    public static T GetTable<T>(TableType type) where T : DataTable
    {
        var id = type;
        if (!tables.ContainsKey(id))
        {
            return null;
        }
        return tables[id] as T;
    }
}
