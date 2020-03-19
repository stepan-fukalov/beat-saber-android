using UnityEngine.UI;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
	private MainUIManager MainManager;
	private string myText;

	private void Start() {
		MainManager = FindObjectOfType<MainUIManager>();
	}

    public void TaskOnClick() {
    	MainManager.SetPlayFile(myText);
    	MainManager.DisablePressedButtonColor();
    	GetComponent<Image>().color = Color.yellow;
    }

    public void SetText(string text) {
    	myText = text;
    	string[] str = myText.Split('/');
    	GetComponentInChildren<Text>().text = str[str.Length - 1]; 
    }
}
