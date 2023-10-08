using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

public class RezenTable : DataTable
{
    public class Data
    {
        public string ID { get; set; }
        public float RezenDuration { get; set; }
    }

    protected Dictionary<string, float> dic = new Dictionary<string, float>();
    public RezenTable()
    {
        path = "Tables/RezenTable";
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
            dic.Add(record.ID, record.RezenDuration);
        }
    }

    public float GetRezenDuration(string id)
    {
        if (!dic.ContainsKey(id))
        {
            return 0f;
        }
        return dic[id];
    }


}
