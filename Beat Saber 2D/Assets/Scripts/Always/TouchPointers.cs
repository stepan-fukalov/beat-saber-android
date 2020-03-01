using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPointers : MonoBehaviour
{
	[SerializeField] private GameObject touchGameObject1, touchGameObject2;
	[SerializeField] private Text text;

	private bool touchCorrect = false;
	private Touch touch1, touch2;

	private void Update() {
		if(Input.touchCount > 1) {
			// if(touchCorrect)
			// 	UpdatePointersPosition();
			// else
			// 	UpdateTouches();
			UpdatePointersPosition();
		} 
		else HidePointers();
	}

	// private void UpdateTouches() {
	// 	touch1 = Input.GetTouch(0);
	// 	touch2 = Input.GetTouch(1);
	// 	float xCenter = Screen.width / 2;
	// 	if(touch1.position.x < xCenter) {
	// 		if(touch2.position.x > xCenter) {
	// 			touchGameObject1.SetActive(true);
	// 			touchGameObject1.SetActive(true);
	// 			touchCorrect = true;
	// 			UpdatePointersPosition();
	// 		}
	// 		else HidePointers();
	// 	}
	// 	else HidePointers();
	// }

	private void UpdatePointersPosition() {
		touchGameObject1.SetActive(true);
		touchGameObject2.SetActive(true);
		touchGameObject1.transform.position = Camera.main.ScreenToWorldPoint(touch1.position);
		touchGameObject2.transform.position = Camera.main.ScreenToWorldPoint(touch2.position);
	}

	private void HidePointers() {
		touchGameObject1.SetActive(false);
		touchGameObject2.SetActive(false);
		touchCorrect = false;
	}
}
