using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/**
 * Author: Zenva 
 * Link: https://www.youtube.com/watch?v=FGh7f-PaGQc
 * **/

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide placement visual
        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // shoot raycast from center of screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits,
            TrackableType.Planes);

        // if AR Plane hit, update position and rotation 
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy) visual.SetActive(true);
        }
    }

}
