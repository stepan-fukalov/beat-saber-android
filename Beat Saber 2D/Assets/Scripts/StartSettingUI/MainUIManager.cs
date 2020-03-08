using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private UIManager[] uiManagers;
    [SerializeField] private Text[] startFroms;
    [SerializeField] private Text[] lengths;
    [SerializeField] private Text[] minValues;
    private AITester tester;

    private void Start() {
        tester = AITester.Instance;
    }

    private void Update() {
        //tester
        // ButtonRandom();
        // ButtonSubmit();
    }

    public void ButtonSubmit() {
        int[] TstartFroms = new int[3];
        int[] Tlengths = new int[3];
        float[,] TminValues = new float[3,4];
    	for(int i = 0; i < uiManagers.Length; i++) 
    	{
    		uiManagers[i].SetEqualizerValues();
            int TstartFrom, Tlength;
            float[] TminValue = new float[4];
            uiManagers[i].GetManagerValues(out TstartFrom, out Tlength, out TminValue);
            TstartFroms[i] = TstartFrom;
            Tlengths[i] = Tlength;
            for(int j = 0; j < 4; j++) {
                TminValues[i, j] = TminValue[j];
    	   }
        }
        tester.SaveSetting(TstartFroms, Tlengths, TminValues);
    	SceneManager.LoadScene("Main");
    }

    public void ButtonRandom() {
    	for(int i = 0; i < 3; i++) {
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
