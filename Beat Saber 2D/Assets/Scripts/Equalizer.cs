using UnityEngine;  
using System.Collections;  
  
public class Equalizer : MonoBehaviour  
{  
    public static Equalizer Instance;
    //An AudioSource object so the music can be played  
    private AudioSource aSource;  
    //A float array that stores the audio samples  
    [SerializeField] private float[] samples = new float[64];  
    [SerializeField] private float[] minSampleValues = new float[12];
    [SerializeField] private float[] maxSampleValues = new float[12];
    [SerializeField] private float[] midSampleValues = new float[64];
    [SerializeField] private float[] min = new float[64];
    [SerializeField] private float[] max = new float[64];

    private float[,] minValueEnableLine = new float[3,4]; 
    private float[,] currentValuesEnableLine = new float[3, 4];

    private int[] startSample = new int[12]; // changed from 3
    private int[] sampleLength = new int[12]; // changed from 3

    private float timeCounter = 10f;

    private bool playing = false;

    private void Awake(){
        if(Instance == null) {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        } 
    }

    private void Start() {
        this.aSource = GetComponent<AudioSource>();          
        aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);
        for(int i = 0; i < samples.Length; i++) {
            min[i] = 1f;
        }
    }

    private void Update ()  
    {  
        if(playing) {
        aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);   
        SetValues();
        UpdateEnableValuesNew();
        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 4; j++) {
                minSampleValues[i * 4 + j] = minValueEnableLine[i, j];
                maxSampleValues[i * 4 + j] = currentValuesEnableLine[i, j];
            }
        }
        // for(int i = 0; i < 3; i++) {
        //     for(int j = 0; j < 4; j++)
        //     currentValuesEnableLine[i, j] = minValueEnableLine[i, j];
        // } 
        if(timeCounter > 0f) timeCounter -= Time.deltaTime;
        else 
            SetMinMaxSampleValues();
        }
    }     

    private void SetValues() {
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

    private void UpdateEnableValuesNew() {
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

    private void SetMinMaxSampleValues() {
        for(int i = 0; i < samples.Length; i++) {
            if(samples[i] < min[i] && samples[i] != 0f) min[i] = samples[i];
            else if(samples[i] > max[i]) max[i] = samples[i];
            midSampleValues[i] = min[i] + (max[i] - min[i]) * 0.23f;
        }

    }

    public void SetSamplesToLines(int line, int startSample, int sampleLength, float[] minValues) {
        // this.startSample[line] = startSample;
        // this.sampleLength[line] = sampleLength;
        // for(int i = 0; i < minValues.Length; i++) {
        //     minValueEnableLine[line, i] = minValues[i];
        // }
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