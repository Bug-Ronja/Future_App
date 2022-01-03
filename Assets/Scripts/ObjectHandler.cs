using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Joystick Lab
 * Link: https://www.youtube.com/watch?v=-Ad_jfl4Wjk
 * **/

public class ObjectHandler : MonoBehaviour
{
    public GameObject objectToSpawn;
    private static ObjectHandler instance;

    public static ObjectHandler Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ObjectHandler>();
            }
            return instance;
        }
    }
}
