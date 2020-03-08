using UnityEngine;  
using System.Collections;  
  
public class Equalizer : MonoBehaviour  
{  
    public static Equalizer Instance;
    //An AudioSource object so the music can be played  
    private AudioSource aSource;  
    //A float array that stores the audio samples  
    private float[] samples = new float[64];  

    private float[,] minValueEnableLine = new float[3,4]; 
    private float[,] currentValuesEnableLine = new float[3, 4];
    private int[] startSample = new int[3];
    private int[] sampleLength = new int[3];

    private bool playing = false;

    private void Awake(){
        if(Instance == null) {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        } 
    }

    private void Start() {
        this.aSource = GetComponent<AudioSource>();          
    }
  
    private void Update ()  
    {  
        if(playing) {
        aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);   
        for(int i = 0; i < 3; i++)
            UpdateEnableValues(i);
        }
    }  

    public void SetSamplesToLines(int line, int startSample, int sampleLength, float[] minValues) {
        this.startSample[line] = startSample;
        this.sampleLength[line] = sampleLength;
        for(int i = 0; i < minValues.Length; i++) {
            minValueEnableLine[line, i] = minValues[i];
        }
    }

    public bool CreateCube(int line, int position) {
        return currentValuesEnableLine[line, position] < minValueEnableLine[line, position] ? false : true;
    }

    //line from 0 to 4, from down to up. position from 0 to 4, from left to right
    private void UpdateEnableValues(int line) {
        int length = sampleLength[line] / 4;
        float[] values = new float[4];
        for(int i = 0; i < 4; i++) {
            float value = 0;
            int startPosInArray = i * length;
            for(int j = startPosInArray + startSample[line]; j < startPosInArray + length + startSample[line]; j++) {
                value += samples[j];
            }
            values[i] = value / length;
            currentValuesEnableLine[line, i] = values[i];
        }
    } 

    public void StartPlay() {
        playing = true;
        aSource.Play();
    }

    public void StopPlay() {
        playing = false;
    }
}  