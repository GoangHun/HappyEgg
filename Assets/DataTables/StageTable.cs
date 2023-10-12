using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using System;

public class StageTable : DataTable
{
	public int StageNum { get; set; }

	public struct LineInfo
	{
		public int Left { get; set; } 
		public int Middle { get; set; }
		public int Right { get; set; }
	}
	public class Data
	{
		public int ID { get; set; }
		public LineInfo LineInfo { get; set; }
	}

	protected Dictionary<int, LineInfo> dic = new Dictionary<int, LineInfo>();
	public StageTable()
	{
		path = "Tables/StageTable" + StageNum;
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
			dic.Add(record.ID, record.LineInfo);
		}
	}

	public LineInfo GetLineInfo(int id)
	{
		if (!dic.ContainsKey(id))
		{
			throw new ArgumentException("StageTable err");
		}
		return dic[id];
	}

}
