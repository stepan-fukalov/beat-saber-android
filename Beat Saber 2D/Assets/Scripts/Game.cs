using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
	public bool Playing { get {return playing;} }
	private bool playing = true;
	private Equalizer equalizer = null;
	private AITester tester = null;
	private SaveAndLoadLevel saveAndLoadLevel;

	private void Awake() {
		equalizer = Equalizer.Instance;
		tester = AITester.Instance;
		saveAndLoadLevel = SaveAndLoadLevel.Instance;
	}

	private void Start() {
		playing = true;
		saveAndLoadLevel.HidePlaySavedLevelButton();

	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			saveAndLoadLevel.Display(load: false);
			Time.timeScale = 0f;
			equalizer.StopPlayingAudio();			
		}
	}

	public void LoadSettingsScene() {
		Destroy(equalizer.gameObject);
    	Destroy(tester.gameObject);
    	Destroy(saveAndLoadLevel.gameObject);
    	Time.timeScale = 1f;
		SceneManager.LoadScene("StartSetting");
	}
}
