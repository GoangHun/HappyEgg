using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using System;

public struct LineInfo
{
	public int Left { get; set; }
	public int Middle { get; set; }
	public int Right { get; set; }

	public LineInfo(int left, int middle, int right)
	{
		Left = left;
		Middle = middle;
		Right = right;
	}
}
public class StageTable : DataTable
{	
	public class Data
	{
		public int ID { get; set; }
		public int Left { get; set; }
		public int Middle { get; set; }
		public int Right { get; set; }
	}

	protected Dictionary<int, LineInfo> dic = new Dictionary<int, LineInfo>();

	public StageTable(string num)
	{
		path = "Tables/SpecialStage" + num;
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
			LineInfo info = new LineInfo(record.Left, record.Middle, record.Right);
			dic.Add(record.ID, info);
		}
	}

	public LineInfo GetLine(int id)
	{
		if (!dic.ContainsKey(id))
		{
			throw new ArgumentException("StageTable err");
		}
		return dic[id];
	}

	public int Count()
	{
		return dic.Count;
	}
}
