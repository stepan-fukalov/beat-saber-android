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

    private Equalizer equalizer = null;
    //private AITester tester = null; 

    private void Start() {
        if(Equalizer.Instance == null)
            Debug.LogError("No equalizer object, try to start from startSettings scene");
        else {
            equalizer = Equalizer.Instance;
            // tester = AITester.Instance;
        }

    	if(game.Playing)
    		StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        equalizer.StartPlayFromCubeSpawner();
    	while(true) {
			for(int i = 0; i < 3; i++) {
                for(int j = 0; j < 4; j++) {
                    if(equalizer.CreateCube(i, j)) 
                        CreateCube(i * 4 + j);
                }
            }
            yield return new WaitForSeconds(delay);  			
				
        	if(Input.GetKeyDown(KeyCode.Keypad0)) CreateCube(0);
    		if(Input.GetKeyDown(KeyCode.Keypad1)) CreateCube(1);
    		if(Input.GetKeyDown(KeyCode.Keypad2)) CreateCube(2);
    		if(Input.GetKeyDown(KeyCode.Keypad3)) CreateCube(3);
    		if(Input.GetKeyDown(KeyCode.Keypad4)) CreateCube(4);
    		if(Input.GetKeyDown(KeyCode.Keypad5)) CreateCube(5);
    		if(Input.GetKeyDown(KeyCode.Keypad6)) CreateCube(6);
    		if(Input.GetKeyDown(KeyCode.Keypad7)) CreateCube(7);
    		if(Input.GetKeyDown(KeyCode.Keypad8)) CreateCube(8);
    		if(Input.GetKeyDown(KeyCode.Keypad9)) CreateCube(9);
    		if(Input.GetKeyDown(KeyCode.Alpha1)) CreateCube(10);
    		if(Input.GetKeyDown(KeyCode.Alpha2)) CreateCube(11);
    		yield return 0;
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
    		GameObject cube = Instantiate(cubePrefab, position, transform.rotation);	
    		int random = Random.Range(0, 2);
    		Team team = (random == 0) ? Team.Red : Team.Blue;
    		cube.GetComponent<Cube>().SetTeam(team);  
    	}
    }
}
