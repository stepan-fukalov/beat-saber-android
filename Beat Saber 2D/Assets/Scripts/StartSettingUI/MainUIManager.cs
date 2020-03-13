using System.Collections;
using System.Collections.Generic;
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
        directoryPath = Path.Combine(Application.persistentDataPath, "Music\\");
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
        float currentPosY = 0;

        foreach(string name in fileNames) {
            string[] str = name.Split('\\');
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
        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio() {
        WWW request = new WWW(fileToPlay);
        yield return request;

        music = request.GetAudioClip();
        equalizer.SetAudioClip(music);
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
