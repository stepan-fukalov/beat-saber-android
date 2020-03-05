using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailTouch : MonoBehaviour
{
	[SerializeField] private Team team = Team.Red;
	private Vector3 previousPosition = Vector3.zero;
	private Vector3 force = Vector3.zero;

	private void Update () {
		force = (transform.position - previousPosition) * Time.deltaTime;
		previousPosition = transform.position;	
	}

    private void OnTriggerEnter(Collider other) {
    	Cube cube = other.GetComponent<Cube>();
    	if(cube != null) {
	    	if(team == cube.CubeTeam)
	    		cube.Kicked(force); 
	    }
    }
}
