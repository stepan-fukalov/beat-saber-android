using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
	private MainUIManager UImanager;

	private void Start() {
		UImanager = FindObjectOfType<MainUIManager>();
	}

    public void TaskOnClick() {
    	GetComponent<Image>().color = Color.yellow;
    	UImanager.SetPlayFile(GetComponentInChildren<Text>().text); 
    }
}
