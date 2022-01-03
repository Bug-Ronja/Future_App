using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: MaxMakesGames
 * Link: https://www.youtube.com/watch?v=P3km_YZFz4A
 * **/

public class ScreenshotHandler : MonoBehaviour
{
    public GameObject UI;

    /**
     * Coroutine for taking screenshots
     * prevents that screenshots are taken during a frame
     * **/
    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        photo.Apply();

        string name = "FuturisticApp" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".png";

        NativeGallery.SaveImageToGallery(photo, "Futuristic App", name);

        Destroy(photo);
        UI.SetActive(true);
    }

    /**
     * Method starts coroutine Screenshot
     * **/
    public void TakeScreenshot()
    {
        UI.SetActive(false);
        StartCoroutine("Screenshot");
    }
}
