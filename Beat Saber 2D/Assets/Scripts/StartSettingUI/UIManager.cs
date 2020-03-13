using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class UIManager : MonoBehaviour
{
	// [SerializeField] private int line;
	// private Equalizer equalizer; 
	// private int startFrom;
	// private int length;
	// private float[] minValues = new float[4];

	// private void Start() {
	// 	equalizer = Equalizer.Instance;
	// }    

	// public void InputStartFrom(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			if(temp >= 0 && temp < 64) {
	// 				startFrom = temp;
	// 				if(length > (63 - startFrom))
	// 					length = 0;					
	// 			} 
	// 			else
	// 				startFrom = 0;
	// 		}
	// 	}
	// 	Debug.Log(startFrom);
	// }

	// public void InputLength(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			if(temp >= 0 && temp < 63 - startFrom) 
	// 				length = temp;
	// 			else
	// 				length = 0;
	// 		}	
	// 	}
	// 	Debug.Log(length);
	// }

	// public void InputMinValue1(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			minValues[0] = (float) temp / Mathf.Pow(10f, value.Length); 
	// 		}
	// 	}
	// 	Debug.Log(minValues[0]);
	// }

	// public void InputMinValue2(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			minValues[1] = (float) temp / Mathf.Pow(10f, value.Length); 
	// 		}
	// 	}
	// 	Debug.Log(minValues[1]);
	// }

	// public void InputMinValue3(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			minValues[2] = (float) temp / Mathf.Pow(10f, value.Length); 
	// 		}
	// 	}
	// 	Debug.Log(minValues[2]);
	// }

	// public void InputMinValue4(string value) {
	// 	if(value != "") {
	// 		int temp = 0;
	// 		if(int.TryParse(value, out temp)) {
	// 			minValues[3] = (float) temp / Mathf.Pow(10f, value.Length); 
	// 		}
	// 	}
	// 	Debug.Log(minValues[3]);
	// }

	// public void SetEqualizerValues() {
	// 	equalizer.SetSamplesToLines(line, startFrom, length, minValues);
	// }

	// public void SetManagerValues(int startFrom, int length, float[] minValues) {
	// 	this.startFrom = startFrom;
	// 	this.length = length;
	// 	this.minValues = minValues;
	// }

	// public void GetManagerValues(out int startFrom, out int length, out float[] minValues) {
	// 	startFrom = this.startFrom;
	// 	length = this.length;
	// 	minValues = this.minValues;
	// }
}
