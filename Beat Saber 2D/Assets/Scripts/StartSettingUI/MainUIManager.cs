using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI; 
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private UIManager[] uiManagers;
    [SerializeField] private Text[] startFroms;
    [SerializeField] private Text[] lengths;
    [SerializeField] private Text[] minValues;

    [SerializeField] private Transform container;
    [SerializeField] private GameObject musicNamePrefab;
    [SerializeField] private Text pathLabel;

    [SerializeField] private Equalizer equalizer;
    private AudioClip music;

    private AITester tester;
    private int fileNameNumber = 0;
    private string directoryPath;
    private string fileToPlay;

    private void Start() {
        tester = AITester.Instance;
        ShowMusicFileNames();
    }

    public void ButtonSubmit() {
        if(fileToPlay != "")
            tester.AnalizeMusic();
        // int[] TstartFroms = new int[3];
        // int[] Tlengths = new int[3];
        // float[,] TminValues = new float[3,4];
        // for(int i = 0; i < uiManagers.Length; i++) 
        // {
        //     uiManagers[i].SetEqualizerValues();
        //     int TstartFrom, Tlength;
        //     float[] TminValue = new float[4];
        //     uiManagers[i].GetManagerValues(out TstartFrom, out Tlength, out TminValue);
        //     TstartFroms[i] = TstartFrom;
        //     Tlengths[i] = Tlength;
        //     for(int j = 0; j < 4; j++) {
        //         TminValues[i, j] = TminValue[j];
        //    }
        // }
        // tester.SaveSetting(TstartFroms, Tlengths, TminValues);
        // SceneManager.LoadScene("Main");
    }

    private void GetMusicFileNames(out List<string> fileNames) {
#if UNITY_ANDROID && !UNITY_EDITOR 
        directoryPath = Path.Combine(Application.persistentDataPath, "Music/");
#else 
        directoryPath = Path.Combine(Application.persistentDataPath, "Music\\");
#endif
        fileNames = new List<string>();
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        else {
            var jsonFiles = Directory.EnumerateFiles(directoryPath, "*.mp3");
            foreach (string filename in jsonFiles) {
                fileNames.Add(filename);
            }
            jsonFiles = Directory.EnumerateFiles(directoryPath, "*.ogg");
            foreach (string filename in jsonFiles) {
                fileNames.Add(filename);
            }
            jsonFiles = Directory.EnumerateFiles(directoryPath, "*.wav");
            foreach (string filename in jsonFiles) {
                fileNames.Add(filename);
            }
        }

        pathLabel.text += directoryPath + ".";
    }

    private void ShowMusicFileNames() {
        List<string> fileNames = new List<string>();
        GetMusicFileNames(out fileNames);
        RectTransform containerRect = container.GetComponent<RectTransform>();
        float prefabHeight = musicNamePrefab.GetComponent<RectTransform>().rect.height;
        float currentPosY = 0f;

        foreach(string name in fileNames) {

#if UNITY_ANDROID && !UNITY_EDITOR 
            string[] str = name.Split('/');
#else 
            string[] str = name.Split('\\');
#endif
            string Tname = str[str.Length - 1];
            containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, containerRect.sizeDelta.y + prefabHeight);
            GameObject obj = Instantiate(musicNamePrefab, container.transform);
            var rct = obj.GetComponent<RectTransform>();
            rct.anchoredPosition = new Vector3(rct.anchoredPosition.x, currentPosY);
            rct.GetComponentInChildren<Text>().text = Tname;
            currentPosY -= prefabHeight;
        }
    }

    public void SetPlayFile(string fileToPlay) {
        this.fileToPlay = directoryPath + fileToPlay;
        Debug.Log("File to play " + fileToPlay + "This file to play " + this.fileToPlay);
        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio() {
        WWW request = new WWW("https://ruru.hotmo.org/get/music/20191214/muzlome_Roddy_Ricch_-_The_Box_67608640.mp3");
        Debug.Log("Request");
        yield return request;
        Debug.Log("Yield return request");
        music = request.GetAudioClip();
        Debug.Log("GetAudioClip");
        equalizer.SetAudioClip(music);
    }

    public void DisablePressedButtonColor() {
        var buttons = container.GetComponentsInChildren<Image>();
        foreach (var button in buttons) 
        {
            button.color = Color.white;
        }
    }

    public void TimeInput(string value) {
        int temp = 0;
         if(value != "") {
             if(int.TryParse(value, out temp)) {
                 tester.TimeCounter = temp;
             }   
         }
         Debug.Log(temp);
    }

    public void MagicNumberInput(string value) {
        int temp = 0;
        if(value != "") {
            if(int.TryParse(value, out temp)) {
                tester.AverageCubeCount = temp;
            }   
        }
        Debug.Log(temp);
    }



    // private void Update() {
    //     //tester
    //     // ButtonRandom();
    //     // ButtonSubmit();
    // }

    

   //  public void ButtonRandom() {
   //  	for(int i = 0; i < 3; i++) {
   //  		int randomStartFrom = Random.Range(0, 64);
   //  		int length = Random.Range(0, 63 - randomStartFrom);
   //  		float[] minValue = new float[4];
   //  		for(int j = 0; j < 4; j++) {
   //  			minValue[j] = Random.Range(0, 1f);
   //  		}
   //  		// Debug.Log(randomStartFrom + " " + length + " " + minValue[0] + " " + minValue[1] + " " + minValue[2] + " " + minValue[3] + " ");
			// uiManagers[i].SetManagerValues(randomStartFrom, length, minValue);
			// for(int j = 4*i; j < 4*(i+1); j++) {
			// 	minValues[j].text = minValue[j%4].ToString();
			// }
			// startFroms[i].text = randomStartFrom.ToString();
			// lengths[i].text = length.ToString();
   //  	}
   //  }

    // public void ButtonLoadSetting() {
    //     int[] TstartFroms = new int[3];
    //     int[] Tlengths = new int[3];
    //     float[] TminValues = new float[12];
    //     tester.DeserializeSetting(out TstartFroms, out Tlengths, out TminValues, fileNameNumber);
    //     for(int i = 0; i < 3; i++) {
    //         for(int j = 4*i; j < 4*(i+1); j++) {
    //             minValues[j].text = TminValues[j].ToString();
    //         }
    //         startFroms[i].text = TstartFroms[i].ToString();
    //         lengths[i].text = Tlengths[i].ToString();
    //     }
    // }

    // public void InputFileNumber(string value) {
    //     if(value != "") {
    //         int temp = 0;
    //         if(int.TryParse(value, out temp)) {
    //             fileNameNumber = temp; 
    //         }
    //     }
    // }
}
