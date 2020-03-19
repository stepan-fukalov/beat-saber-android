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

	private void Awake() {
		equalizer = Equalizer.Instance;
		tester = AITester.Instance;
	}

	private void Start() {
		playing = true;
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {			
    		SceneManager.LoadScene("StartSetting");

		}
	}

	private void OnDestroy() {
		playing = false;
		equalizer.StopPlayFromGame();
		tester.ResetVariables();
	}
}
