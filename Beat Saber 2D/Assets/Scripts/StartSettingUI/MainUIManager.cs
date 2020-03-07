using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private UIManager[] uiManagers;
    [SerializeField] private Text[] startFroms;
    [SerializeField] private Text[] lengths;
    [SerializeField] private Text[] minValues;

    public async void ButtonSubmit() {
    	foreach (UIManager ma in uiManagers) 
    	{
    		ma.SetEqualizerValues();
    	}
    	var asyncLoad = SceneManager.LoadSceneAsync("Main");
        while (!asyncLoad.isDone) await Task.Delay(15);
    }

    public void ButtonRandom() {
    	for(int i = 0; i < 4; i++) {
    		int randomStartFrom = Random.Range(0, 64);
    		int length = Random.Range(0, 63 - randomStartFrom);
    		float[] minValue = new float[4];
    		for(int j = 0; j < 4; j++) {
    			minValue[j] = Random.Range(0, 1f);
    		}
    		// Debug.Log(randomStartFrom + " " + length + " " + minValue[0] + " " + minValue[1] + " " + minValue[2] + " " + minValue[3] + " ");
			uiManagers[i].SetManagerValues(randomStartFrom, length, minValue);
			for(int j = 4*i; j < 4*(i+1); j++) {
				minValues[j].text = minValue[j%4].ToString();
			}
			startFroms[i].text = randomStartFrom.ToString();
			lengths[i].text = length.ToString();
    	}
    }
}
