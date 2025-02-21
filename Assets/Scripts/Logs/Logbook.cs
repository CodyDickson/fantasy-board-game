using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logbook : MonoBehaviour
{
    public static bool updateLogbook = false;
    public static string itemOne = "";
    public static string itemTwo = "";
    public static string itemThree = "";

    void Start()
    {
        
    }

    void Update()
    {
        if (updateLogbook)
        {
            //
            updateLogbook = false;
        }
    }

    public static void AddToLogbook()
    {
        // 
    }
}