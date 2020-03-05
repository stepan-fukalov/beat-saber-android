using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource[] sounds;

    private void Awake() {
    	if(Instance == null)
    		Instance = this;
    }

    public void PlaySound(int index) {
    	sounds[index].Play();
    }
}
