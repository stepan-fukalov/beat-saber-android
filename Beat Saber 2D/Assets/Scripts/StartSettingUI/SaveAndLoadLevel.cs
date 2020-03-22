using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class SaveAndLoadLevel : MonoBehaviour
{
	public static SaveAndLoadLevel Instance = null;

	[Header("Load or Save")]
	[SerializeField] private bool load;

	[Space]

	[Header("Variables")] 
	[SerializeField] private Slot[] slots = new Slot[5];
	[SerializeField] private GameObject container;
	[SerializeField] private GameObject buttonPlaySavedLevel;
	[SerializeField] private Text adviceText;
	public GameObject loadingScreen;

	private string directoryPath;
	private string audioName;
	private string fullAudioURL;
	public string AudioName { 
		set {
    		string[] str = value.Split('/');
    		audioName = str[str.Length - 1];
    		audioName = audioName.Substring(0, 30);
    		fullAudioURL = value;
		}
	}

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	private void Start() {
		DeserializeSlots();
	}

	private void DeserializeSlots() {
		directoryPath = Path.Combine(Application.persistentDataPath, "Saved Levels");
		if(!Directory.Exists(directoryPath))
			Directory.CreateDirectory(directoryPath);
		var files = Directory.EnumerateFiles(directoryPath, "*.json");
		foreach (string jsonFile in files) 
		{

			int slotNumber = int.Parse(jsonFile[jsonFile.Length - 6].ToString());
			string rawJson = File.ReadAllText(jsonFile);
			var node = JSON.Parse(rawJson);
			slots[slotNumber].SetValues(node);
		}
	}

    public void Display(bool load) {
    	container.SetActive(true);
    	this.load = load;
    	if(load) 
    		adviceText.text = "Load level you want to play.";
    	else 
    		adviceText.text = "Would you like to save this level?";
    }

    public void ButtonSlot(int slotNumber) {
    	if(load) {
    		if(slots[slotNumber].slotSet) {
    			slots[slotNumber].LoadLevel();
    			ButtonClose();
    		}
    	}
    	else {
    		SaveSlot(slotNumber);	
    		ButtonClose();
    	} 	
    }

    private void SaveSlot(int slotNumber) {
    	string filePath = Path.Combine(directoryPath, slotNumber + ".json");
    	var node = new JSONObject();
    	node["name"] = audioName;
    	node["fullAudioURL"] = fullAudioURL;
    	node["time"] = AITester.Instance.TimeCounter;
    	node["rate"] = AITester.Instance.Rate;
    	node["magicNumber"] = AITester.Instance.AverageCubeCount;
    	File.WriteAllText(filePath, node.ToString());
    }

	public void ButtonClose() {
		container.SetActive(false);
		if(!load) {
			Game game = FindObjectOfType<Game>();
			game.LoadSettingsScene();
		}
	}

	public void HidePlaySavedLevelButton() {
		buttonPlaySavedLevel.SetActive(false);
	}

	public void DisplayPlaySavedLevelButton() {
		buttonPlaySavedLevel.SetActive(true);
	}
}

[System.Serializable]
    public class Slot {
    	[SerializeField] private Text slotText;
    	[HideInInspector] public bool slotSet = false;
    	private JSONNode node;
    	public void SetValues(JSONNode node) {
    		slotSet = true;
    		this.node = node;
    		string name = node["name"];
    		int time = node["time"];
    		int magicNumber = node["magicNumber"];
    		slotText.text = string.Format(name + " " + time + " " + magicNumber);
    	}

    	public void LoadLevel() {
    		AITester.Instance.AverageCubeCount = node["magicNumber"];
    		AITester.Instance.TimeCounter = node["time"];
    		AITester.Instance.Rate = node["rate"];
    		MainUIManager.Instance.SetPlayFile(node["fullAudioURL"]);
    	}
    }
