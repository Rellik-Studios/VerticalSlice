using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(System.DateTime.Now.TimeOfDay);
        Debug.Log("The date is " + System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
        string theDate = System.DateTime.Now.ToString("hh:mm");
        //Debug.Log("The time is" + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute);
        Debug.Log("The time is " + theDate);

        string userName = Environment.UserName;

        if(userName != null)
            Debug.Log("You are defined as" + userName);
        Debug.Log("Your device name is: " + SystemInfo.deviceName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
