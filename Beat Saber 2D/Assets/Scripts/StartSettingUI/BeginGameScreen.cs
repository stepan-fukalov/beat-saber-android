using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BeginGameScreen : MonoBehaviour
{
	[SerializeField] private Color redColor;
	[SerializeField] private Color blueColor;
	[SerializeField] private Image[] cubes;
	[SerializeField] private Text magicNumberText;
	private int magicNumber;

	public void Display(int magicNumber) {
		gameObject.SetActive(true);
		this.magicNumber = magicNumber;
		magicNumberText.text += magicNumber.ToString() + ".";
	}

	public void Hide() {
		gameObject.SetActive(false);
		magicNumberText.text.Substring(0, magicNumberText.text.Length - magicNumber.ToString().Length - 1);
	}

	public void UpdateCubeDisplay(int currentMagicNumber) {
		HideCubeDisplay();
		List<int> posibleNumbers = new List<int>();
		for(int i = 0; i < cubes.Length; i++) {
			posibleNumbers.Add(i);
			// Debug.Log(i);
		}
		for(int i = 0; i < currentMagicNumber; i++) {
			int random = Random.Range(0, posibleNumbers.Count);
			// Debug.Log("random " + random);
			int cubeNumber = posibleNumbers[random];
			// Debug.Log("cubeNumber " + cubeNumber);
			posibleNumbers.RemoveAt(random);
			cubes[cubeNumber].color = (Random.Range(0, 2) == 0) ? redColor : blueColor;
		}
	}

	private void HideCubeDisplay() {
		foreach (Image cube in cubes) 
		{
			cube.color = new Color(cube.color.r, cube.color.g, cube.color.b, 0);
		}
	}
}
