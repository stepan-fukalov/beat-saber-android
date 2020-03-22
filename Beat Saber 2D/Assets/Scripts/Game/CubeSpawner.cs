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
    private List<Cube> previousCubes = new List<Cube>();
    private List<Cube> currentCubes = new List<Cube>();
    private AITester tester = null; 

    private void Start() {
        if(Equalizer.Instance == null)
            Debug.LogError("No equalizer object, try to start from startSettings scene");
        else {
            equalizer = Equalizer.Instance;
            tester = AITester.Instance;
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
                        CreateCubePrefab(i * 4 + j);
                }
            }
            RedesigneCubes();
            SetPreviousCubes();
            yield return new WaitForSeconds(delay);  			
   		}
    }

    private void RedesigneCubes() {
        int extraCubes = currentCubes.Count - tester.AverageCubeCount;
        for(int i = 0; i < extraCubes; i++) {
            Destroy(currentCubes[0].gameObject);
            currentCubes.Remove(currentCubes[0]);
        }

        foreach (Cube curCube in currentCubes) 
        {
            curCube.SetTeam(GetCubeTeam(curCube.Index));
            foreach (Cube prCube in previousCubes) 
                {
                    if(prCube.Index == curCube.Index)
                        if(Random.Range(0, 3) == 0) {
                            bool notCollide = true;
                            foreach(Cube TcurCube in currentCubes) {
                                if(GetOppositeIndex(curCube.Index) == TcurCube.Index) {
                                    notCollide = false;
                                    break;
                                }
                            }
                            if(notCollide) {
                                curCube.SetTeam((curCube.CubeTeam == Team.Red) ? Team.Blue : Team.Red);
                                curCube.Index = GetOppositeIndex(curCube.Index);
                            }
                        }
                        // else {
                        //     if(curCube.CubeTeam == prCube.CubeTeam && Random.Range(0, 3) == 0) 
                        //         curCube.SetTeam((prCube.CubeTeam == Team.Red) ? Team.Blue : Team.Red);
                        // }
                }
            curCube.gameObject.transform.position = GetCubePosition(curCube.Index);
        }
        if(Random.Range(0, 5) == 0)
            foreach (Cube curCube in currentCubes) 
            {
                curCube.SetTeam(GetCubeTeam(GetOppositeIndex(curCube.Index))); // switch color to opposite
            }
        
    }

    private void CreateCubePrefab(int index) {
        Cube cube = Instantiate(cubePrefab, transform.position, transform.rotation).GetComponent<Cube>();    
        cube.Index = index;
        currentCubes.Add(cube);
    }

    private Vector3 GetCubePosition(int index) {
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
        return position;
    }

    private Team GetCubeTeam(int index) {
        if((index % 4) <= 1)
            return Team.Red;
        else 
            return Team.Blue;
    }

    private void SetPreviousCubes() {
        previousCubes = currentCubes;
        currentCubes.Clear();
    }

    private int GetOppositeIndex(int index) {
        if(index % 4 == 0) index += 3;
        else if(index % 4 == 3) index -= 3;
        else if(index % 4 == 1) index += 1;
        else index -= 1;
        return index;
    }
}
