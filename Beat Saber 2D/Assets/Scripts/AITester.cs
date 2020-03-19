using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AITester : MonoBehaviour
{
	public static AITester Instance; 

	// private int filesNumber = 0;
	// private float timeCounter = 1.5f;
	// private JSONObject node;
	// private bool goodSetting = false;
	// private string filepath;
	// private string directoryPath;
	// private int loadSettingNumber;

	[SerializeField] private float[] min = new float[64];
    [SerializeField] private float[] max = new float[64];
    [SerializeField] private int timeCounter;
    // private float timeCounterCounter;
    [SerializeField] private float analizeTime = 5f;
    private float analizeTimeCounter;
    [SerializeField] private float spawnDelay = 1f; // should be equaled delay in cubeSpawner
    private float spawnDelayCounter;
    [SerializeField] private float rate = 0.23f;
    private float additiveRate;
    [SerializeField] private int averageCubeCount = 4;

	private float[] midSampleValues = new float[64];
	private float[] samples = new float[64];

	private Equalizer equalizer;
	private int cubeCount;
	private int spawnCount;
	public bool Analizing { get; private set; }
	public int TimeCounter { set { timeCounter = value; }}
	public int AverageCubeCount {set { averageCubeCount = value; } get {return averageCubeCount; }}

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	private void Start() {
		equalizer = Equalizer.Instance;
		// timeCounterCounter = timeCounter;
		analizeTimeCounter = analizeTime;
		spawnDelayCounter = spawnDelay;
		additiveRate = rate;
		Analizing = true;
	}

	private void Update() {

		if(equalizer.ASource.isPlaying && Analizing) {
			equalizer.ASource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);
			SetMinMaxSampleValues();
			analizeTimeCounter -= Time.deltaTime;
			if(analizeTimeCounter < 0f)
				StopAnalizeMusic();
		}
	}

	public void AnalizeMusic() {
		for(int i = 0; i < samples.Length; i++) {
            min[i] = 1f;
        }
        equalizer.StartPlay();
        equalizer.ASource.time = (float) timeCounter;
	}

	private void StopAnalizeMusic() {
		equalizer.StopPlay();
		analizeTimeCounter = analizeTime;
		// timeCounterCounter = timeCounter;		
		AnalizeCubeSpawns();
	}

	private void AnalizeCubeSpawns() {
		cubeCount = cubeCount / spawnCount;
		additiveRate = rate / 2;
		if(cubeCount == averageCubeCount || additiveRate < 0.02)
			LoadMainScene();
		else {
			if(cubeCount < averageCubeCount)
				rate -= additiveRate;
			else
				rate += additiveRate;
			cubeCount = 0;
			spawnCount = 0;
			AnalizeMusic();
		} 
	}

	private void LoadMainScene() {
		equalizer.StopPlay();				
		Analizing = false;
		SceneManager.LoadScene("Main");
	}

	private void SetMinMaxSampleValues() {
			for(int i = 0; i < samples.Length; i++) {
		        if(samples[i] < min[i] && samples[i] != 0f) min[i] = samples[i];
		        else if(samples[i] > max[i]) max[i] = samples[i];
		        midSampleValues[i] = min[i] + (max[i] - min[i]) * rate;
		}
		if(spawnDelayCounter > 0) spawnDelayCounter -= Time.deltaTime;
		else {
			equalizer.SetMidValues(midSampleValues);
			TestCubeAndSpawnNumber();
		}
    }

    private void TestCubeAndSpawnNumber() {
    	spawnCount++;
    	int createCount = 0;
    	for(int i = 0; i < 3; i++) {
    		for(int j = 0; j < 4; j++) {
    			if(equalizer.CreateCube(i, j)) {
    				createCount++;
    			}
    		}
    	}
    	cubeCount += createCount;
    	spawnDelayCounter = spawnDelay; 
    	Debug.Log(cubeCount + " " + spawnCount);
    }

    public void ResetVariables() {
    	Analizing = false;
    }

	// private void Start() {
	// 	directoryPath = Path.Combine(Application.persistentDataPath, "JSON Save data");
	// 	if(Directory.Exists(directoryPath))
	// 	{
	// 		var jsonFiles = Directory.EnumerateFiles(directoryPath, "*.json");
				
	// 		foreach (string filepath in jsonFiles) 
	// 		{
	// 			filesNumber++;
	// 		}			
	// 	}
	// 	else {
	// 		Debug.Log("Directory created: " + directoryPath);
	// 		Directory.CreateDirectory(directoryPath);
	// 		filesNumber = 0;	
	// 	}
	// }

	// private void WriteJSON(string node) {
	// 	if(!Directory.Exists(directoryPath))
	// 	{
	// 		Debug.Log("Directory created: " + directoryPath);
	// 		Directory.CreateDirectory(directoryPath);
	// 	}
	// 	filepath = Path.Combine(directoryPath,  + filesNumber + ".json");
	// 	File.WriteAllText(filepath, node);
	// 	filesNumber++;
	// }

	// public void SaveSetting(int[] startFroms, int[] lengths, float[,] values) {
	// 	node = new JSONObject();
	// 	for(int i = 0; i < 3; i++) {
	// 		node["startFrom" + i.ToString()] = startFroms[i];
	// 		node["length" + i.ToString()] = lengths[i];
	// 		for(int j = 0; j < 4; j++) {
	// 			node["value" + i.ToString() + j.ToString()] = values[i,j];
	// 		}
	// 	}
	// 	WriteJSON(node.ToString());
	// }

	// public void SaveCubeSpawner(bool[] cubeCreated, float timeSpend) {
	// 	timeCounter -= timeSpend;
	// 	if(timeCounter < 0) {
	// 		timeCounter = 1.5f;
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
	// 			createdTimes = 0;
	// 		}
	// 		if(goodLines == 3) goodSetting = true;
	// 	}
	//  } 

	//  public void DeserializeSetting(out int[] startFroms, out int[] lengths, out float[] minValues, int fileNameNumber) {
	//  	directoryPath = Path.Combine(Application.persistentDataPath, "JSON Save data");
	//  	filepath = Path.Combine(directoryPath, + fileNameNumber + ".json");
	// 	string rawJson = File.ReadAllText(filepath);
 //      	var node = JSON.Parse(rawJson);
 //      	int[] TstartFroms = new int[3];
 //      	int[] Tlengths = new int[3];
 //      	float[] TminValues = new float[12];			
 //      	for(int i = 0; i < 3; i++) {
	// 		TstartFroms[i] = node["startFrom" + i.ToString()];
	// 		Tlengths[i] = node["length" + i.ToString()];
	// 		for(int j = 0; j < 4; j++) {
	// 			TminValues[i * 4 + j] = node["value" + i.ToString() + j.ToString()];
	// 		}
	// 	}
	// 	startFroms = TstartFroms;
	// 	lengths = Tlengths;
	// 	minValues = TminValues;
	// 	// loadSettingNumber++;
	// 	// if(loadSettingNumber >= jsonFiles.Length) loadSettingNumber = 0;	
	// }

	// public int GetSettingNumber() {
		// return loadSettingNumber;
	// }
}
