using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class ObstacleTable : DataTable
{
    public class Data
    {
        public int StageNum { get; set; }
        public int ObstacleCount { get; set; }
    }

    protected Dictionary<int, int> dic = new Dictionary<int, int>();
    public ObstacleTable()
    {
        path = "Tables/ObstacleTable";
        //Path.Combine(Application.dataPath, "Tables/StringTable.csv");	//경로 수정
        Load();
    }
    public override void Load()
    {
        var csvStr = Resources.Load<TextAsset>(path);   //path확장자를 지워줘야 함. Resources폴더에 저장
                                                        //string csvFileText = File.ReadAllText(path);

        TextReader reader = new StringReader(csvStr.text);

        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        var records = csv.GetRecords<Data>();

        foreach (var record in records)
        {
            dic.Add(record.StageNum, record.ObstacleCount);
        }
    }

    public int GetObstacleCount(int id)
    {
        if (!dic.ContainsKey(id))
        {
            return 0;
        }
        return dic[id];
    }

    public int GetCount()
    {
        return dic.Count;
    }

}
