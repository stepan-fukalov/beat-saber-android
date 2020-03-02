using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
    	Debug.Log("Hit");
    	Destroy(other.gameObject);
    }
}
