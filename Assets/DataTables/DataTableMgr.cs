using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTableMgr
{
    //Key�� System.Type�� ���
    private static Dictionary<System.Type, DataTable> tables = new Dictionary<System.Type, DataTable>();

    static DataTableMgr()
    {
        tables.Clear();

        var rezenTable = new RezenTable();
        tables.Add(typeof(RezenTable), rezenTable);
        var obstacleTable = new ObstacleTable();
        tables.Add(typeof(ObstacleTable), obstacleTable);
    }

    public static T GetTable<T>() where T : DataTable
    {
        var id = typeof(T);
        if (!tables.ContainsKey(id))
        {
            return null;
        }
        return tables[id] as T;
    }
}
