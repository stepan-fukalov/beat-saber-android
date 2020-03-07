using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
	public bool Playing { get; } = true;

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {			
    		SceneManager.LoadScene("StartSetting");
		}
	}
}
