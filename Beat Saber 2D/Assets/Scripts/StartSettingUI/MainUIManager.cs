using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI; 
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject musicNamePrefab;
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Equalizer equalizer;
    private AudioClip music;

    private AITester tester;
    private string urlOfAudio;

    private void Start() {
        tester = AITester.Instance;
    }

    public void ButtonSubmit() {
        if(!String.IsNullOrEmpty(urlOfAudio)) {
            tester.AnalizeMusic();
        }
    }

    private void ShowMusicURLs(Stack<string> urls) {
        RectTransform containerRect = container.GetComponent<RectTransform>();
        float prefabHeight = musicNamePrefab.GetComponent<RectTransform>().rect.height;
        foreach(SelectButton musicPrefab in container.GetComponentsInChildren<SelectButton>()) {
            Destroy(musicPrefab.gameObject);
        }
        foreach(string name in urls) {
            GameObject obj = Instantiate(musicNamePrefab, container.transform);
            containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, containerRect.sizeDelta.y + prefabHeight);
            obj.GetComponent<SelectButton>().SetText(name);
        } 
    }

    public void SetPlayFile(string urlOfAudio) {
        this.urlOfAudio = urlOfAudio;
        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio() {
        using (WWW request = new WWW(urlOfAudio)) {
            loadingScreen.SetActive(true);
            yield return request;
            loadingScreen.SetActive(false);    
            if(!String.IsNullOrEmpty(request.error)) {
                NotificationManager.Instance.CreateErrorNotification("Invalid URL");
                urlOfAudio = "";
            }
            else {
                music = request.GetAudioClip();            
                equalizer.SetAudioClip(music);    
            }
        }
   }

   private IEnumerator SiteRequest(string value) {
        using (WWW request = new WWW(value)) {
            loadingScreen.SetActive(true);
            yield return request;    
            if(!String.IsNullOrEmpty(request.error)) {
                NotificationManager.Instance.CreateErrorNotification("Invalid site link");
            }
            else {
                SeekAudioFiles(request.text);
            }
            loadingScreen.SetActive(false);
        } 
   }

   private void SeekAudioFiles(string data) {

        int length = data.Length > 100000 ? 100000 : data.Length;
        Stack<string> audio = new Stack<string>();
        data = data.Substring(0, length);
        string str = data;
        while(str.IndexOf(".mp3\"") != -1) {
            int mp3 = str.LastIndexOf(".mp3\"");
            str = str.Substring(0, mp3 + 4);
            int href = str.LastIndexOf("http");
            if(href == -1) 
                break;
            if(str.Substring(href + 4, 4).Contains("://"))
                audio.Push(str.Substring(href, mp3 - href + 4));
            str = str.Substring(0, href);
        }
        ShowMusicURLs(audio);
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

    public void URLInput(string value) {
        if(!String.IsNullOrEmpty(value)) {
            SetPlayFile(value);
        }
    }

    public void SiteInput(string value) {
        if(!String.IsNullOrEmpty(value)) {
            StartCoroutine(SiteRequest(value));
        }
    }
}