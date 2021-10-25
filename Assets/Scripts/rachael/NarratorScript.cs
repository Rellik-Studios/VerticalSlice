using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorScript : MonoBehaviour
{
    public static string m_date;

    public static string m_time;

    public static string m_userName;

    public static string m_deviceName;

    public static string m_weekDay;
    void Start()
    {
        m_userName = Environment.UserName;
        m_deviceName = SystemInfo.deviceName;

        m_weekDay = System.DateTime.Now.DayOfWeek.ToString();
        
        if(m_userName != null)
            Debug.Log("You are defined as" + m_userName);
        Debug.Log("Your device name is: " + SystemInfo.deviceName);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_date = System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;
        m_time = System.DateTime.Now.ToString("h:mm tt");
    }
}
