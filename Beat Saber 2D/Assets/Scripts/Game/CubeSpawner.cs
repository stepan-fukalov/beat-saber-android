using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	[SerializeField] private Game game = null;

	[SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float delay = 1f;

    private void Start() {
    	if(game.Playing)
    		StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        // for(int y = 0; y <= 2; y++) {
        //     Vector3 position = new Vector3(transform.position.x + xOffset * 1, transform.position.y + yOffset * y, transform.position.z);
        //     Instantiate(cubePrefab, position, Quaternion.identity);
        //     position = new Vector3(transform.position.x + xOffset * 3, transform.position.y + yOffset * y, transform.position.z);
        //     Instantiate(cubePrefab, position, Quaternion.identity);
        //     position = new Vector3(transform.position.x + xOffset * -1, transform.position.y + yOffset * y, transform.position.z);
        //     Instantiate(cubePrefab, position, Quaternion.identity);
        //     position = new Vector3(transform.position.x + xOffset * -3, transform.position.y + yOffset * y, transform.position.z);
        //     Instantiate(cubePrefab, position, Quaternion.identity);
        // }
        int i = 0;
        while(true) {
        	i++;
        	if(i > 11) i = 0;
        	CreateCube(i);
	        yield return new WaitForSeconds(delay);
        }
    }

    private void CreateCube(int index) {
    	if(index < 0 && index > 11)
    		Debug.LogError("Cube of this index can not be created");
    	else {
    		int y = index / 4;
    		int x = index % 4;
    		Vector3 position = Vector3.zero;
    		switch (x) 
    		{
    			case 0:
    			  position = new Vector3(transform.position.x + xOffset * -3, transform.position.y + yOffset * y, transform.position.z);
    			  break;
    			case 1:
    			  position = new Vector3(transform.position.x + xOffset * -1, transform.position.y + yOffset * y, transform.position.z);    			  
    			  break;
    			case 2:
    			  position = new Vector3(transform.position.x + xOffset * 1, transform.position.y + yOffset * y, transform.position.z);    			  
    			  break;
    			case 3:
    			  position = new Vector3(transform.position.x + xOffset * 3, transform.position.y + yOffset * y, transform.position.z);    			  
    			  break;
    		}
    		Instantiate(cubePrefab, position, Quaternion.identity);	
    	}
    }
}
