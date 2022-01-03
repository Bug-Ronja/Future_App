using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Author: Joystick Lab
 * Link: https://www.youtube.com/watch?v=-Ad_jfl4Wjk
 * Customised and expanded by: Ronja Bergemann
 * **/

/**
 * ButtonManager for calling the functions when a Button is pressed
 * assignment from functions to buttons in the scene 
 * **/
public class ButtonManager : MonoBehaviour
{
    public GameObject chapterMenu;
    public GameObject readerPlayer;
    public GameObject warning;
    public GameObject shareMenu;
    public GameObject favDisabled;

    private ObjectSpawner objectSpawner;
    private AudioManager audioManager;
    private SetUpArticle setUpArticle;
    private ScreenshotHandler screenshotHandler;

    // Start is called before the first frame update
    private void Start()
    {
        // works for now, needs to be changed when more than one Edition is added
        if (SceneManager.GetActiveScene().name.Equals("Edition 1"))
        {
            audioManager = FindObjectOfType<AudioManager>();
            setUpArticle = GetComponent<SetUpArticle>();

            chapterMenu.SetActive(false);
            readerPlayer.SetActive(false);
            shareMenu.SetActive(false);
        } 
        else if (SceneManager.GetActiveScene().name.Equals("ImageTracking") || SceneManager.GetActiveScene().name.Equals("PlaneTracking"))
        {
            objectSpawner = FindObjectOfType<ObjectSpawner>();
            screenshotHandler = GetComponent<ScreenshotHandler>();
        } 

        if (SceneManager.GetActiveScene().name.Equals("ImageTracking"))
        {
            warning.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectObject(GameObject objectToSpawn)
    {
        ObjectHandler.Instance.objectToSpawn = objectToSpawn;
    }

    public void Delete()
    {
        objectSpawner.DeleteAllObjects();
    }

    public void OpenChapterMenu()
    {
        chapterMenu.SetActive(true);
    }

    public void CloseChapterMenu()
    {
        chapterMenu.SetActive(false);
    }

    public void DissmissWarning()
    {
        warning.SetActive(false);
    }

    public void OpenReaderPlayer()
    {
        readerPlayer.SetActive(true);
    }

    public void CloseReaderPlayer()
    {
        readerPlayer.SetActive(false);
    }

    public void PlayFile()
    {
        audioManager.PlayFile();
    }

    public void StopFile()
    {
        audioManager.StopFile();
    }

    public void SkipToStart()
    {
        audioManager.SkipToStart();
    }

    public void SkipToEnd()
    {
        audioManager.SkipToEnd();
    }

    public void OpenShareMenu()
    {
        shareMenu.SetActive(true);
    }

    public void CloseShareMenu()
    {
        shareMenu.SetActive(false);
    }

    public void CopyToClipboard()
    {
        setUpArticle.CopyToClipboard();
    }

    public void TakeScreenshot()
    {
        screenshotHandler.TakeScreenshot();
    }
}
