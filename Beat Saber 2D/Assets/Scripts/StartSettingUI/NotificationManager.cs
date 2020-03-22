using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    [SerializeField] private Transform notification;

    private void Awake() {
    	if(Instance == null)
    		Instance = this;
    }

    public void CreateErrorNotification(string message) {
    	Transform obj = Instantiate(notification, transform.position, transform.rotation);
    	obj.SetParent(transform);
    	obj.GetComponentInChildren<Text>().text = message;
    	StartCoroutine(Destroy(obj));
    }

    private IEnumerator Destroy(Transform obj) {
    	yield return new WaitForSeconds(2f);
    	var textColor = obj.GetComponentInChildren<Text>();
    	var imColor = obj.GetComponent<Image>();
    	for(float i = 1; i >= 0; i -= Time.deltaTime) {    		
    		textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, i);
    		imColor.color = new Color(imColor.color.r, imColor.color.g, imColor.color.b, i);
    		yield return null;
    	}
    	Destroy(obj.gameObject);
    }
}
