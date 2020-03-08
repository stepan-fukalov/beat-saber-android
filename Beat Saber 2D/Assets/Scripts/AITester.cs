using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AITester : MonoBehaviour
{
	public static AITester Instance; 

	private int filesNumber = 0;
	private float timeCounter = 1f;
	private JSONObject node;
	private bool goodSetting = false;
	private string filepath;
	private string directoryPath;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	private void Start() {
		// directoryPath = Path.Combine(Application.persistentDataPath, "JSON Save data");
		// if(Directory.Exists(directoryPath))
		// {
		// 	var jsonFiles = Directory.EnumerateFiles(directoryPath, "*.json");
				
		// 	foreach (string filepath in jsonFiles) 
		// 	{
		// 		filesNumber++;
		// 	}			
		// }
		// else {
		// 	Debug.Log("Directory created: " + directoryPath);
		// 	Directory.CreateDirectory(directoryPath);
		// 	filesNumber = 0;	
		// }
	}

	private void WriteJSON(string node) {
		// directoryPath = Path.Combine(Application.persistentDataPath, "JSON Save data");
		// if(!Directory.Exists(directoryPath))
		// {
		// 	Debug.Log("Directory created: " + directoryPath);
		// 	Directory.CreateDirectory(directoryPath);
		// }
		// filepath = Path.Combine(directoryPath,  + filesNumber + ".json");
		// File.WriteAllText(filepath, node);
		// filesNumber++;
	}

	public void SaveSetting(int[] startFroms, int[] lengths, float[,] values) {
		// node = new JSONObject();
		// for(int i = 0; i < 3; i++) {
		// 	node["startFrom" + i.ToString()] = startFroms[i];
		// 	node["length" + i.ToString()] = lengths[i];
		// 	for(int j = 0; j < 4; j++) {
		// 		node["value" + i.ToString() + j.ToString()] = values[i,j];
		// 	}
		// }
		// WriteJSON(node.ToString());
	}

	public void SaveCubeSpawner(bool[] cubeCreated, float timeSpend) {
	// 	timeCounter -= timeSpend;
	// 	if(timeCounter < 0) {
	// 		timeCounter = 1f;
	// 		if(!goodSetting) {
	// 			filepath = Path.Combine(directoryPath, + (filesNumber - 1) + ".json");
	// 			File.Delete(filepath); 
	// 		}
	// 		goodSetting = false;
	// 		SceneManager.LoadScene("StartSetting");
	// 	}
	// 	else {
	// 		int createdTimes = 0;
	// 		int goodLines = 0;
	// 		for(int i = 0; i < 3; i++) {
	// 			for(int j = 0; j < 4; j++) {
	// 				if(cubeCreated[i * 4 + j]) createdTimes++;
	// 			}
	// 			if(createdTimes > 0 && createdTimes < 4) goodLines++;
	// 		}
	// 		if(goodLines == 3) goodSetting = true;
	// 	}
	 } 
}
