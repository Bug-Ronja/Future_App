using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Autor: Joystick Lab
 * Link: https://www.youtube.com/watch?v=-Ad_jfl4Wjk
 * Nachprogrammiert und angepasst von: Ronja Bergemann
 * **/
public class ObjectManager : MonoBehaviour
{
   
    public void SelectObject(GameObject objectToSpawn)
    {
        ObjectHandler.Instance.objectToSpawn = objectToSpawn;
    }

}
