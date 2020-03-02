using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPointers : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private GameObject touchGameObject1, touchGameObject2;
	[SerializeField] private Text text;

	private bool touchCorrect = false, redFirst = false;
	private Touch touch1, touch2;
	private Vector3 touch1Position, touch2Position;

	private void Awake() {
		cam.enabled = false;
	}

	private void Update() {
		if(Input.touchCount > 1) {
				UpdateTouches();
		} 
		else HidePointers();
	}

	private void UpdateTouches() {
		touch1 = Input.GetTouch(0);
		touch2 = Input.GetTouch(1);
		touch1Position = cam.ScreenToWorldPoint(touch1.position);
		touch2Position = cam.ScreenToWorldPoint(touch2.position);
		if(!touchCorrect) {
			if(touch1Position.x < 0f) {
				if(touch2Position.x > 0f) {
					touchGameObject1.SetActive(true);
					touchGameObject2.SetActive(true);
					touchCorrect = true;
					redFirst = true;
					UpdateTouchPositions(redFirst);
				}
				else HidePointers();
			}
			else if(touch1Position.x > 0f) {
				if(touch2Position.x < 0f) {
					touchGameObject1.SetActive(true);
					touchGameObject2.SetActive(true);
					touchCorrect = true;
					redFirst = false;
					UpdateTouchPositions(redFirst);
				}
				else HidePointers();
			}
			else HidePointers();
		}
		else {
			UpdateTouchPositions(redFirst);
		}
	}

	private void UpdateTouchPositions(bool red) {
		if(red) {
			touchGameObject1.transform.position = new Vector3(touch1Position.x, touch1Position.y, -2f);
			touchGameObject2.transform.position = new Vector3(touch2Position.x, touch2Position.y, -2f);
		}
		else {
			touchGameObject1.transform.position = new Vector3(touch2Position.x, touch2Position.y, -2f);
			touchGameObject2.transform.position = new Vector3(touch1Position.x, touch1Position.y, -2f);
		}
	}

	private void HidePointers() {
		touchGameObject1.SetActive(false);
		touchGameObject2.SetActive(false);
		touchCorrect = false;
	}
}
