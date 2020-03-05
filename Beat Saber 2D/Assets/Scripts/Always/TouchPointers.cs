using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPointers : MonoBehaviour
{
	[SerializeField] private Camera cam;
	[SerializeField] private GameObject cursor1, cursor2;
	[SerializeField] private Text text;
	[SerializeField] private GameObject touchCollider1, touchCollider2;

	[SerializeField] private float zCursorOffset = -2f; 
	[SerializeField] private float zTouchTrailOffset = -9f;
	[SerializeField] private float xTouchTrailOffset, yTouchTrailOffset;

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
		//UpdateDebug();
	}

	// private void UpdateDebug() {
	// 	var mouse = Input.mousePosition;
	// 	var position = cam.ScreenToWorldPoint(mouse);
	// 	cursor1.transform.position = new Vector3(position.x, position.y, zCursorOffset);
	// 	var cursorPosition = cursor1.transform.position;
	// 	touchCollider1.transform.position = new Vector3(cursorPosition.x * xTouchTrailOffset, cursorPosition.y * yTouchTrailOffset, zTouchTrailOffset);
	// }

	private void UpdateTouches() {
		touch1 = Input.GetTouch(0);
		touch2 = Input.GetTouch(1);
		touch1Position = cam.ScreenToWorldPoint(touch1.position);
		touch2Position = cam.ScreenToWorldPoint(touch2.position);
		if(!touchCorrect) {
			if(touch1Position.x < 0f) {
				if(touch2Position.x > 0f) {
					cursor1.SetActive(true);
					cursor2.SetActive(true);
					touchCollider1.SetActive(true);
					touchCollider2.SetActive(true);
					touchCorrect = true;
					redFirst = true;
					UpdateTouchPositions(redFirst);
				}
				else HidePointers();
			}
			else if(touch1Position.x > 0f) {
				if(touch2Position.x < 0f) {
					cursor1.SetActive(true);
					cursor2.SetActive(true);
					touchCollider1.SetActive(true);
					touchCollider2.SetActive(true);
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
			cursor1.transform.position = new Vector3(touch1Position.x, touch1Position.y, zCursorOffset);
			cursor2.transform.position = new Vector3(touch2Position.x, touch2Position.y, zCursorOffset);
		}
		else {
			cursor1.transform.position = new Vector3(touch2Position.x, touch2Position.y, zCursorOffset);
			cursor2.transform.position = new Vector3(touch1Position.x, touch1Position.y, zCursorOffset);
		}
		Vector3 cursor1Position = cursor1.transform.position;
		Vector3 cursor2Position = cursor2.transform.position;

		touchCollider1.transform.position = new Vector3(cursor1Position.x * xTouchTrailOffset, cursor1Position.y * yTouchTrailOffset, zTouchTrailOffset);
		touchCollider2.transform.position = new Vector3(cursor2Position.x * xTouchTrailOffset, cursor2Position.y * yTouchTrailOffset, zTouchTrailOffset);
	}

	private void HidePointers() {
		cursor1.SetActive(false);
		cursor2.SetActive(false);
		touchCollider1.SetActive(false);
		touchCollider2.SetActive(false);
		touchCorrect = false;
	}
}
