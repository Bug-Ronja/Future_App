using UnityEngine;
using UnityEngine.Video;

/**
 * Author: Creationno Tech Solutions
 * Link: https://www.youtube.com/watch?v=JyNatFqYHK8
 * **/
public class VideoPlaneController : MonoBehaviour
{
    public VideoPlayer vp;

    private void OnEnable()
    {
        if (vp != null) vp.Play();
    }

    private void OnDisable()
    {
        if (vp != null) vp.Stop();
    }
}
