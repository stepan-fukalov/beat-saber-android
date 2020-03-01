using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float moveSpeed, zCameraOffset;

    private void Update() {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed); 
		if(transform.position.z < Camera.main.transform.position.z - zCameraOffset)
			Destroy(gameObject, 1f);	
    }
}
