using UnityEngine;  
using System.Collections;  
  
public class Equalizer : MonoBehaviour  
{  
    public static Equalizer Instance;
    //An AudioSource object so the music can be played  
    public AudioSource ASource { get { return audioToAnalize; } }  
    [SerializeField] private AudioSource audioToAnalize;
    [SerializeField] private AudioSource audioToPlay;
    [SerializeField] private float waitBeforeMusicPlay;
    //A float array that stores the audio samples  
    [SerializeField] private float[] samples = new float[64];  
    [SerializeField] private float[] midSampleValues = new float[64];

    [SerializeField] private float[,] minValueEnableLine = new float[3,4]; 
    [SerializeField] private float[,] currentValuesEnableLine = new float[3, 4];

    // private int[] startSample = new int[12]; // changed from 3
    // private int[] sampleLength = new int[12]; // changed from 3

    // private float timeCounter = 10f;

    private bool playing = false;

    private void Awake(){
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
    }

    private void Update ()  
    {  
        if(playing) {
            ASource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);
            UpdateMinValues();
            UpdateCurrentValues();                
        } 
    }     

    private void UpdateMinValues() {
        int Tline, TstartSample, TsampleLength;
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 4; j++) {
                Tline = i * 4 + j;
                float Tvalue = 0;
                if(Tline != 10 && Tline != 11) {
                    TstartSample = Tline * 6;
                    TsampleLength = 6;
                } 
                else if(Tline == 10) {
                    TstartSample = 60;
                    TsampleLength = 2;
                }
                else {
                    TstartSample = 62;
                    TsampleLength = 2;
                }

                for(int x = TstartSample; x < (TsampleLength + TstartSample); x++) {
                        Tvalue += midSampleValues[x];
                    }
                minValueEnableLine[i, j] = Tvalue / TsampleLength;
            }
        }
    }

    private void UpdateCurrentValues() {
        int Tline, TstartSample, TsampleLength;
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 4; j++) {
                Tline = i * 4 + j;
                float Tvalue = 0;
                if(Tline != 10 && Tline != 11) {
                    TstartSample = Tline * 6;
                    TsampleLength = 6;
                } 
                else if(Tline == 10) {
                    TstartSample = 60;
                    TsampleLength = 2;
                }
                else {
                    TstartSample = 62;
                    TsampleLength = 2;
                }

                for(int x = TstartSample; x < (TsampleLength + TstartSample); x++) {
                        Tvalue += samples[x];
                    }
                currentValuesEnableLine[i, j] = Tvalue / TsampleLength;
            }
        }
    }

    public void SetMidValues(float[] midValue) {
        midSampleValues = midValue;
    }

    public bool CreateCube(int line, int position) {
        return currentValuesEnableLine[line, position] < minValueEnableLine[line, position] ? false : true;
    }

    public void StartPlay() {
        playing = true;
        ASource.Play();
    }

    public void StopPlay() {
        playing = false;
    }

    public void StartPlayFromCubeSpawner() {
        StartCoroutine(WaitMusic());
    }

    private IEnumerator WaitMusic() {
        playing = true;
        ASource.Play();
        yield return new WaitForSeconds(waitBeforeMusicPlay);
        audioToPlay.clip = ASource.clip;
        audioToPlay.Play();
    }

    public void SetAudioClip(AudioClip audioClip) {
        ASource.clip = audioClip;
        Debug.Log("SetAudioClip");
    }

    // public void SetSamplesToLines(int line, int startSample, int sampleLength, float[] minValues) {
    //     // this.startSample[line] = startSample;
    //     // this.sampleLength[line] = sampleLength;
    //     // for(int i = 0; i < minValues.Length; i++) {
    //     //     minValueEnableLine[line, i] = minValues[i];
    //     // }
    // }

    //line from 0 to 4, from down to up. position from 0 to 4, from left to right
    // private void UpdateEnableValues(int line) {
    //     int length = sampleLength[line] / 4;
    //     float[] values = new float[4];
    //     for(int i = 0; i < 4; i++) {
    //         float value = 0;
    //         int startPosInArray = i * length;
    //         for(int j = startPosInArray + startSample[line]; j < startPosInArray + length + startSample[line]; j++) {
    //             value += samples[j];
    //         }
    //         values[i] = value / length;
    //         currentValuesEnableLine[line, i] = values[i];
    //     }
    // } 

    
}  