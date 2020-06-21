using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TimeManager : MonoBehaviour
{

    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter,
    }

    [SerializeField]
    public float LocalTimeScale = 1.0f; //现实时间几秒等于一分钟
    public int Year;
    public Season Season_Enum;
    public int Date;
    public int Hour;
    public int Minute;
    public string Season_Str;
    public Flowchart SetVariable;

    private float tempTime;

    // Use this for initialization
    void Awake()
    {
        DateInit();
        DateUpdate();

        tempTime = 0;
        //InvokeRepeating("MinuteRun", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //暂停
        if (PauseManager.instance.IsPause)
        {
            return;
        }

        tempTime += Time.deltaTime;
        if (tempTime >= LocalTimeScale)
        {
            MinuteRun();
            tempTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Date += 1;
        }
    }

    public void MinuteRun()
    {
        Minute++;
        DateUpdate();
        SetFungusVar();
    }

    private void DateUpdate()
    {
        if (Minute >= 60)
        {
            Hour += 1;
            Minute = 0;
        }

        if (Hour >= 24)
        {
            Date += 1;
            Hour = 0;
        }

        if (Date > 30)
        {
            if (Season_Enum == Season.Winter)
            {
                Season_Enum = Season.Spring;
                Year += 1;
                Date = 1;
            }
            else
            {
                Season_Enum += 1;
                Date = 1;
            }
        }

        switch (Season_Enum)
        {
            case Season.Spring:
                Season_Str = "春";
                break;
            case Season.Summer:
                Season_Str = "夏";
                break;
            case Season.Fall:
                Season_Str = "秋";
                break;
            case Season.Winter:
                Season_Str = "冬";
                break;
        }
    }

    public void DateInit()
    {
        Year = 1;
        Season_Enum = Season.Spring;
        Date = 1;
        Hour = 6;
        Minute = 0;
    }

    public void DateInitInMorning()
    {
        Hour = 6;
        Minute = 0;
    }

    public void SetDate(int year, string season, int date, int hour, int minute)
    {
        if (season.Equals("Spring"))
        {
            Season_Enum = Season.Spring;
        }
        else if (season.Equals("Summer"))
        {
            Season_Enum = Season.Summer;
        }
        else if (season.Equals("Fall"))
        {
            Season_Enum = Season.Fall;
        }
        else if (season.Equals("Winter"))
        {
            Season_Enum = Season.Winter;
        }
        else
        {
            Debug.Log("error: Season not exist");
        }

        Year = year;
        Date = date;
        Hour = hour;
        Minute = minute;
    }

    private void SetFungusVar()
    {
        SetVariable.SetIntegerVariable("TempTime", Hour);
        Flowchart.BroadcastFungusMessage("SetVariable");
    }
}
