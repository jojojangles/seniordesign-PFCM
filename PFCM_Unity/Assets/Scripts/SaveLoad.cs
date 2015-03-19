using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static List<Character> savedCharacters = new List<Character>();

	public static void Save() {
		savedCharacters.Add(Game.curChar);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedChars.pfc");
		bf.Serialize(file, SaveLoad.savedCharacters);
		file.Close();
	}

	public static void Load() {
		if(File.Exists (Application.persistentDataPath + "/savedChars.pfc"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedChars.pfc", FileMode.Open);
			SaveLoad.savedCharacters = (List<Character>)bf.Deserialize(file);
			file.Close();
		}
	}

	public static void SaveChar(Character c) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedChars/" + c.cname() + ".pfc");
		bf.Serialize(file, c);
		file.Close();
	}
}
