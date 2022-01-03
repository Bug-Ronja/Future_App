using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

/**
 * Author: Dev Enabled
 * Link: https://www.youtube.com/watch?v=I9j3MD7gS5Y
 * Customised by: Ronja Bergemann
 * **/

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placableObjects;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    // Awake function is called before Start function
    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject obj in placableObjects)
        {
            // Insatnciate Object: Vector3.zero = starts hidden(not working?), Quaternion.identity = deafault rotation
            GameObject newObj = Instantiate(obj, Vector3.zero, Quaternion.identity);
            newObj.name = obj.name;
            spawnedObjects.Add(obj.name, newObj);

            // hide object visual
            newObj.SetActive(false);
        }
    }

    /**
     * Method binds event to the trackedImageChanged in the trackedImageManager
     * **/
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    /**
     * Method unbinds event to the trackedImageChanged in the trackedImageManager
     * **/
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
    
    /**
     * Method allows to call functionality based on if an Image is added,
     * updated or removed
     * **/
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.name].SetActive(false);
        }
    }

    /**
     * Method activates GameObject based on the trackedImage
     * **/
    private void UpdateImage (ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject obj = spawnedObjects[name];
        obj.transform.position = position;
        obj.SetActive(true);

        foreach(GameObject gameObj in spawnedObjects.Values)
        {
            if(gameObj.name != name)
            {
                gameObj.SetActive(false);
            }
        }
    }

}
