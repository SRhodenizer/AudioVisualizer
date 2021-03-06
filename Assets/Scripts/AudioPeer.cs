﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    private AudioSource audioSource;
    private float[] freqBands = new float[8];
    private float[] bandsBuffer = new float[8];
    private float[] bufferDecrease = new float[8];
    private float[] freqBandHigh = new float[8];
    private string songName = "";

    public static float[] samples = new float[512];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static float amplitude, amplitudeBuffer;
    float amplitudeHighest;

    //array of audio tracks
    public AudioClip[] tracks = new AudioClip[8];
    //which audio track is currently playing 
    public int currTrack = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = tracks[0];
        songName = "<size=50><color=#ffffff>" + tracks[currTrack % 7].name + "</color></size>";
        GUIStyle style = new GUIStyle();
        style.richText = true;
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }

        GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().enabled = true;
        GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().Inactive();
        GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().Active();
        GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().enabled = false;
        GameObject.Find("SphereRight").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("SphereLeft").GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        /*
         * 22050 / 512 = 43 hertz per sample
         * 
         * 7 categories
         * 20 - 60 hertz
         * 60 - 250 hertz
         * 250 - 500 hertz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 4000 - 6000 hertz
         * 6000 - 20000 hertz
         * 
         * 8 channels
         * 0 - 2 = 86 hertz
         * 1 - 4 = 172 hertz - 87 - 258 
         * 2 - 8 = 344 hertz - 259 - 602
         * 3 - 16 = 688 hertz - 603 - 1290
         * 4 - 32 = 1376 hertz - 1291 - 2666
         * 5 - 64 = 2752 hertz - 2667 - 5418
         * 6 - 128 = 5504 hertz - 5419 - 10922
         * 7 - 256 = 11008 hertz - 10923 - 21930
         * 510
         */

        int count = 0;

        for(int i = 0; i < freqBands.Length; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            float average = 0;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for(int i = 0; i < bandsBuffer.Length; i++)
        {
            if(freqBands[i] > bandsBuffer[i])
            {
                bandsBuffer[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }

            if(freqBands[i] < bandsBuffer[i])
            {
                bandsBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if(freqBands[i] > freqBandHigh[i])
            {
                freqBandHigh[i] = freqBands[i];
            }

            audioBand[i] = (freqBands[i] / freqBandHigh[i]);
            audioBandBuffer[i] = (bandsBuffer[i] / freqBandHigh[i]);
        }
    }

    void GetAmplitude()
    {
        float currAmp = 0;
        float currBuff = 0;
        for (int i = 0; i < audioBand.Length; i++)
        {
            currAmp += audioBand[i];
            currBuff += audioBandBuffer[i];
        }

        if (currAmp > amplitudeHighest)
        {
            amplitudeHighest = currAmp;
        }

        amplitude = currAmp / amplitudeHighest;
        amplitudeBuffer = currBuff / amplitudeHighest;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 100, 30), "Change Track")) {
            //up the track
            currTrack++;
            audioSource.clip = tracks[currTrack % 7];
            //make sure it's playing 
            audioSource.Play();

            songName = "<size=50><color=#ffffff>" + tracks[currTrack % 7].name + "</color></size>";

            Debug.Log("CurrTrack: " + (currTrack % 7));

            if ((currTrack % 7) <= 4)
            {
                GameObject[] flyArray = GameObject.FindGameObjectsWithTag("scoutFly");

                for(int i = 0; i < flyArray.Length; i++)
                {
                    Destroy(flyArray[i]);
                }
                /*
                GameObject[] tetronimoSphere = GameObject.FindGameObjectsWithTag("tetronimoNC");

                Debug.Log("setting true");

                for (int i = 0; i < tetronimoSphere.Length; i++)
                {
                    tetronimoSphere[i].SetActive(true);
                }
                */
                
                GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().enabled = true;
                GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().Inactive();
                GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().Active();
                GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().enabled = false;
                GameObject.Find("SphereRight").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("SphereLeft").GetComponent<MeshRenderer>().enabled = false;

                /*
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("monster");

                for(int i = 0; i < monsters.Length; i++)
                {
                    monsters[i].SetActive(false);
                }
                */
            }
            else
            {
                GameObject[] blockArray = GameObject.FindGameObjectsWithTag("tetronimo");

                for (int i = 0; i < blockArray.Length; i++)
                {
                    Destroy(blockArray[i]);
                }

                GameObject[] tetronimoSphere = GameObject.FindGameObjectsWithTag("tetronimoNC");

                for (int i = 0; i < tetronimoSphere.Length; i++)
                {
                    tetronimoSphere[i].SetActive(false);
                }

                /*
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("monster");

                for (int i = 0; i < monsters.Length; i++)
                {
                    monsters[i].SetActive(true);
                }
                */

                GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().enabled = false;
                GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().Active();
                GameObject.Find("Instantiate Cubes").GetComponent<TetrisBackground>().Inactive();
                GameObject.Find("Instantiate Cubes").GetComponent<MonsterHunterBackground>().enabled = true;
                GameObject.Find("SphereRight").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("SphereLeft").GetComponent<MeshRenderer>().enabled = true;
            }
        }

        if (GUI.Button(new Rect(10, 110, 100, 30), "Pause/Play"))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.Play();
            }

        }

        GUIStyle style = new GUIStyle();
        style.richText = true;
        GUILayout.Label(songName, style);
    }
}
