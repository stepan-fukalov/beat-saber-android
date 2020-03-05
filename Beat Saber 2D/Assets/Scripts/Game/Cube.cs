using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float moveSpeed, zCameraOffset;
    [SerializeField] private Color red = Color.red;
    [SerializeField] private Color blue = Color.blue;
    [SerializeField] private float destroyIn = 2f, zKickDirection = 0.2f;
    [SerializeField] private float xAddForce = 0.2f, yAddForce = 0.2f, minZForce = 5f;

    private Rigidbody rb;
    private bool kicked = false;
    public Team CubeTeam { get; private set; }

    public Vector3 RbVelocity {get {return rb.velocity; } }

    private void Awake() {
    	rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() { 
    	if(!kicked) {
			rb.velocity = new Vector3(0f, 0f, moveSpeed);
			if(transform.position.z < zCameraOffset) {
                AudioManager.Instance.PlaySound(0);
                Destroy(gameObject);    
            }
  		}	
    }

    public void Kicked(Vector3 origin) {
    	kicked = true;
    	rb.useGravity = true;
        float zForce;
        if(Mathf.Abs(origin.x * origin.y) < minZForce) {
            zForce = minZForce;
            AudioManager.Instance.PlaySound(2);   
        }
        else {
            zForce = Mathf.Abs(origin.x * origin.y);
            AudioManager.Instance.PlaySound(1);
        }
    	Vector3 direction = new Vector3(origin.x * xAddForce, origin.y * yAddForce, zForce * zKickDirection);
    	rb.AddForce(direction, ForceMode.Impulse);
        rb.AddTorque(direction, ForceMode.Impulse);
    	Destroy(gameObject, destroyIn);
    }

    public void WrongColor() {

    }

    public void SetTeam(Team team) {
        CubeTeam = team;
    	switch (team) 
    	{
    		case Team.Red:
    		  GetComponent<Renderer>().material.color = red;
    		  break;
    		case Team.Blue:
    		  GetComponent<Renderer>().material.color = blue;
    		 break;
    	}
    }
}

public enum Team { Red, Blue }