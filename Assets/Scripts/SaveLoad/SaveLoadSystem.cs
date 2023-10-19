using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV1;//새버전 나올때마다 업데이트

public static class SaveLoadSystem
{
	//게임 오브젝트없이 단독으로 돌아도되는 애들 static 하면 좋음
	public static int SaveDataVersion { get; } = 1;//새버전 나올때마다 업데이트

	public static string SaveDirectory
	{
		get
		{
			return $"{Application.persistentDataPath}/Save";
		}
	}

	public static void Save(SaveData data, string filename)//세이브 데이터의 내용을  filename에다 저장함
	{
		//지정된 경로가 디스크에 있는 기존 디렉터리를 참조하는지를 확인
		if (!Directory.Exists(SaveDirectory))
		{
			Directory.CreateDirectory(SaveDirectory);
		}

		var path = Path.Combine(SaveDirectory, filename);
		using (var writer = new JsonTextWriter(new StreamWriter(path)))
		{
			var serializer = new JsonSerializer();
			serializer.Converters.Add(new Vector3Converter());  //Collection을 상속 받고 있어서 Add가 가능
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
			version = jObg["Version"].Value<int>();//.Value<T> 어떤데이터형을 가져오냐에 따라서 
		}
		using (var reader = new JsonTextReader(new StringReader(json)))
		{
			var serialize = new JsonSerializer();
			switch (version)//새버전 나올때마다 추가
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
