using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV1;//������ ���ö����� ������Ʈ

public static class SaveLoadSystem
{
	//���� ������Ʈ���� �ܵ����� ���Ƶ��Ǵ� �ֵ� static �ϸ� ����
	public static int SaveDataVersion { get; } = 1;//������ ���ö����� ������Ʈ

	public static string SaveDirectory
	{
		get
		{
			return $"{Application.persistentDataPath}/Save";
		}
	}

	public static void Save(SaveData data, string filename)//���̺� �������� ������  filename���� ������
	{
		//������ ��ΰ� ��ũ�� �ִ� ���� ���͸��� �����ϴ����� Ȯ��
		if (!Directory.Exists(SaveDirectory))
		{
			Directory.CreateDirectory(SaveDirectory);
		}

		var path = Path.Combine(SaveDirectory, filename);
		using (var writer = new JsonTextWriter(new StreamWriter(path)))
		{
			var serializer = new JsonSerializer();
			serializer.Converters.Add(new Vector3Converter());  //Collection�� ��� �ް� �־ Add�� ����
			serializer.Converters.Add(new QuaternionConverter());
			serializer.Serialize(writer, data);
		}
	}

	public static SaveData Load(string filename)
	{

		var path = Path.Combine(SaveDirectory, filename);
		if (!File.Exists(path))
		{
			return null;
		}
		SaveData data = null;
		int version = 0;
		var json = File.ReadAllText(path);

		using (var reader = new JsonTextReader(new StringReader(json)))
		{
			var jObg = JObject.Load(reader);
			version = jObg["Version"].Value<int>();//.Value<T> ����������� �������Ŀ� ���� 
		}
		using (var reader = new JsonTextReader(new StringReader(json)))
		{
			var serialize = new JsonSerializer();
			switch (version)//������ ���ö����� �߰�
			{
				case 1:
					data = serialize.Deserialize<SaveDataV1>(reader);
					break;
			}

			while (data.Version < SaveDataVersion)
			{
				data = data.VersionUp();
			}
		}

		return data;
	}
}