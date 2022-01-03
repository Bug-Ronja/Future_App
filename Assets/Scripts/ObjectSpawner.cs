using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Authors: Zenva, Joystick Lab und Dev Enabled
 * Link Zenva: https://www.youtube.com/watch?v=FGh7f-PaGQc
 * Link Joystick Lab: https://www.youtube.com/watch?v=-Ad_jfl4Wjk
 *                    https://www.youtube.com/watch?v=A7woL0oZCnA
 * Link Dev Enabled: https://www.youtube.com/watch?v=phDbAMYVkzw
 * Customised and expanded by: Ronja Bergemann
 * **/

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    private PlacementIndicator placementIndicator;
    private Touch touch;
    private List<GameObject> placedObjectsList = new List<GameObject>();

    [SerializeField]
    private int maxObjectSpawnCount = 0;
    private int placedObjectCount;

    // Start is called before the first frame update
    private void Start()
    {
        // get component 
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Gets first Touch on the screen
        touch = Input.GetTouch(0);

        if (IsPointerOverUI(touch))
        {
            return;
        } 
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (placedObjectCount < maxObjectSpawnCount)
            {
                objectToSpawn = Instantiate(ObjectHandler.Instance.objectToSpawn, placementIndicator.transform.position,
                    placementIndicator.transform.rotation);
                placedObjectsList.Add(objectToSpawn);
                placedObjectCount++;
            }
            else return;
        }
    }

    /**
     * Method checks if touch on the screen is on the UI
     * **/
    bool IsPointerOverUI (Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    /**
     * Method deletes all objects in the placedObjectList
     * **/
    public void DeleteAllObjects()
    {
        if (placedObjectsList.Count > 0)
        {
            foreach (GameObject obj in placedObjectsList)
            {
                Destroy(obj);
            }
            EmptyPlacedObjects();
        }
    }

    /**
     * Method clears the placedOIbjectList and resets the placedObjectCount
     * **/
    private void EmptyPlacedObjects()
    {
        placedObjectsList.Clear();
        placedObjectCount = 0;
    }

}
