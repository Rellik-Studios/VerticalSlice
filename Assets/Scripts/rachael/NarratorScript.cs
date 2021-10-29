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

    public static string timeCategory
    {
        get
        {
            string cat;
            int hour = DateTime.Now.Hour;
            cat = hour >= 6 && hour < 12 ? "Morning" :
                hour < 18 && hour >= 12 ? "Afternoon" :
                hour >= 18 && hour < 21 ? "Evening" : "Night";
           
            return cat;
        }
    }

    public static string activity
    {
        get
        {
            int hour = DateTime.Now.Hour;
            return hour >= 5 && hour < 9 ? "You're up early, aren't you." :
                hour < 17 && hour >= 9 ?   "Shouldn't you be working at this time? Or is 9 to 5 not your style?" :
                hour >= 17 && hour < 20 ?  "Now's a good time to go outside, enjoy the daylight... why are you here?" :
                hour >= 20 && hour < 22 ?  "This time in the evening is perfect for playing video games, I'd say." :
                                           "It's awfully late. What's wrong, can't sleep?";
        }
    }
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