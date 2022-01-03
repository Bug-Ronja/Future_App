using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/**
 * Authors: xOctoManx und gamesplusjames
 * Link xOctoManx: https://www.youtube.com/watch?v=zpiwhC8zp4A&list=PL2d40w4fhSCxMjMMp6R14MlvbTaHK8HeY
 * Link gamesplusjames: https://www.youtube.com/watch?v=xFc6AsHMkK8&list=PLiyfvmtjWC_X6e0EYLPczO9tNCkm2dzkm
 * Customised by: Ronja Bergemann
 * **/

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public AudioClip[] textToSpeechFiles;

    public GameObject headline;

    public GameObject title;
    public GameObject fullTime;
    public GameObject progressTime;

    public GameObject playButton;
    public GameObject stopButton;
    public GameObject skipEndButton;
    public GameObject skipBeginningButton;
    public Slider progressBar;

    private int fullLength;
    private int playTime;
    private int seconds;
    private int minutes;

    private AudioSource source;

    private JSONReader jsonReader;

    // Update is called once per frame
    private void Update()
    {
        playTime = (int)source.time;
        ShowPlayTime();
    }

    /**
     * Method starts and sets up the AudioManager
     * **/
    public void StartAudioManager()
    {
        jsonReader = FindObjectOfType<JSONReader>();
        source = GetComponent<AudioSource>();

        playButton.SetActive(false);
        stopButton.SetActive(false);
        skipBeginningButton.SetActive(false);
        skipEndButton.SetActive(false);

        progressBar.maxValue = playTime;
        progressBar.minValue = 0;

        CurrentTitle();
        ShowCurrentFile();
    }

    /**
     * Method plays Audiofile
     * **/
    public void PlayFile()
    {
        playButton.SetActive(false);
        stopButton.SetActive(true);

        if(source.isPlaying)
        {
            return;
        }

        StartCoroutine("WaitForFileEnd");

        source.Play();
    }
    
    /**
     * Coroutine for playing File
     * prevents that two audio files play at the same time
     * **/
    IEnumerator WaitForFileEnd()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
    }

    /**
     * Method sets up the reader-window
     * **/
    private void CurrentTitle()
    {
        string fileName = "";
        string headlineText = "";

        headlineText = headline.transform.GetComponent<TMPro.TextMeshProUGUI>().text;

        for (int i = 0; i < jsonReader.contentList.GetLenght(); i++)
        {
            if(headlineText.Equals(jsonReader.contentList.GetCurrentContent(i).title))
            {
                fileName = jsonReader.GetCurrentContent(i).title;
            } 
        }

        for(int i = 0; i < textToSpeechFiles.Length; i++)
        {
            if(fileName.Equals(textToSpeechFiles[i].name))
            {
                source.clip = textToSpeechFiles[i];

                playButton.SetActive(true);
                skipBeginningButton.SetActive(true);
                skipEndButton.SetActive(true);
            }
        }

        // TO DO: NOT WORKING - EDIT OR DELETE LATER
        if(!playButton.activeInHierarchy)
        {
            title.GetComponent<TMPro.TextMeshProUGUI>().text = "No Text to Speech available";
            progressTime.GetComponent<TMPro.TextMeshProUGUI>().text = "0:00";
            fullTime.GetComponent<TMPro.TextMeshProUGUI>().text = "0:00";
        }
    }

    /**
     * Method pauses currently playing audio file
     * **/
    public void StopFile()
    {
        StopCoroutine("WaitForFileEnd");
        source.Pause();
        stopButton.SetActive(false);
        playButton.SetActive(true);
    }

    /**
     * Methid sets up the title of reader-window
     * sets fullLength
     * **/
    private void ShowCurrentFile()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = source.clip.name;
        fullLength = (int)source.clip.length;
    }

    /**
     * Method sets up the time display on the reader-window
     * also sets up the max, min and current value of progressBar slider
     * **/
    private void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        
        progressTime.GetComponent<TMPro.TextMeshProUGUI>().text = minutes + ":" + seconds.ToString("D2");
        fullTime.GetComponent<TMPro.TextMeshProUGUI>().text = (fullLength / 60) % 60 + ":" + (fullLength % 60).ToString("D2");

        progressBar.maxValue = fullLength;
        progressBar.minValue = 0;
        progressBar.value = playTime;
    }

    /**
     * Method sets time of audio file to start
     * **/
    public void SkipToStart()
    {
        source.time = 0;
    }

    /**
     * Method skips audio file to end
     * also sets audio file to start, so it can be played properly again
     * **/
    public void SkipToEnd()
    {
        source.time = fullLength;
        stopButton.SetActive(false);
        playButton.SetActive(true);
        source.time = 0;
        source.Stop();
    }

}
