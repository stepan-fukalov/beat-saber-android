using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
	private MainUIManager MainManager;

	private void Start() {
		MainManager = FindObjectOfType<MainUIManager>();
	}

    public void TaskOnClick() {
    	MainManager.SetPlayFile(GetComponentInChildren<Text>().text);
    	MainManager.DisablePressedButtonColor();
    	GetComponent<Image>().color = Color.yellow;
    }
}
