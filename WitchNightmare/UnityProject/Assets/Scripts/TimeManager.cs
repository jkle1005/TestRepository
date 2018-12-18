using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Witch;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if (TimeManager.instance == null)
                TimeManager.instance = FindObjectOfType<TimeManager>();
            return TimeManager.instance;
        }
    }

    public float ratio;
    
    

    [Serializable]
    public class TimeData
    {
        public  int  time;
        public  int  day;
        public  int  month;
        public  int  week;

        public  List<int>  weather;
        public  int    rainy_count;

        public  int    moonphase;
        

        public string GetMWD()
        {
            return month.ToString() + "." + week.ToString() + "." + day.ToString();
        }

        public string GetTime()
        {
            int hour = time / 6;
            int minute = time % 6 * 10;

            string str_h = hour.ToString();
            string str_minute = minute.ToString();

            if (hour < 10)
                str_h = "0" + str_h;

            if (minute == 0)
                str_minute = "00";

            return str_h + ":" + str_minute;
        }

        public string GetWeather()
        {
            string result = " - ";

            foreach(int num in weather)
            {
                result += num.ToString();
                result += " - ";
            }

            return result;
        }

        public int SetWeather(int _weather)
        {
            int result = 0;

            Debug.Log(rainy_count.ToString());

            switch (_weather)
            {
                case 0:
                    if (rainy_count >= 40)
                        result = Utility.Instance.Choose(new int[] { 100, 20 + rainy_count / 2, rainy_count });
                    else
                        result = 0;
                    break;

                case 1:
                    result = 2;                    
                    break;

                case 2:
                    if (rainy_count >= 30)
                    {
                        result = Utility.Instance.Choose(new int[] { 50, 0, rainy_count * 3 });
                        rainy_count = 0;
                    }                        
                    else
                        result = 0;
                    break;

                default:
                    Debug.LogError("TimeData::SetWeather(int) int = " + _weather.ToString());
                    break;
            }


            switch (result)
            {
                case 0:
                    rainy_count += 10;
                    break;

                case 1:
                    rainy_count += 20;
                    break;

                case 2:
                    rainy_count -= 50;
                    if (rainy_count < 0)
                        rainy_count = 0;
                    break;
            }

            return result;
        }        

        public void EndDay()
        {
            // 
            // 날씨 변화
            weather.RemoveAt(0);
            weather.Add(SetWeather(weather[weather.Count - 1]));

            // 위상 변화
            if(day == 1)
            {
                moonphase = 0;
            }
            else if(day <= 5)
            {
                moonphase = 1;
            }
            else if(day <= 10)
            {
                moonphase = 2;
            }
            else if(day <= 14)
            {
                moonphase = 3;
            }
            else if(day == 15)
            {
                moonphase = 4;
            }
            else if(day <= 18)
            {
                moonphase = 5;
            }
            else if(day <= 23)
            {
                moonphase = 6;
            }
            else if(day <= 27)
            {
                moonphase = 7;
            }
            else if(day == 28)
            {
                moonphase = 0;
            }
        }

        public void InitData()
        {
            time = 48;
            day = 1;
            month = 1;
            week = 1;
            rainy_count = 20;

            weather = new List<int>();
            weather.Add(0);
            for (int i = 0; i < 6; i++)
            {
                weather.Add(SetWeather(weather[weather.Count - 1]));
            }        

            moonphase = 0;
        }
    }

    private  TimeData  timeData;
    private  float     time_now;
    private  bool      time_pause;
    public   bool      Time_pause { get { return time_pause; } }


    public void Pause()
    {
        time_pause = true;
    }

    public void Resume()
    {
        time_pause = false;
    }

    private void EndDay()
    {
        timeData.EndDay();

        //
        // 작물 성장
        // 필드 변화
    }

    private void TimeFlow()
    {
        timeData.time++;

        if(timeData.time == 144)
        {
            timeData.time = 0;
            timeData.day++;
            EndDay();

            if(timeData.day > 1 && timeData.day % 7 == 1)
            {                
                timeData.week++;

                if(timeData.week == 5)
                {
                    timeData.week = 1;
                    timeData.day = 1;
                    timeData.month++;

                    if(timeData.month == 13)
                    {
                        timeData.month = 1;
                    }
                }                
            }
        }

        UIControl.Instance.SetTime(timeData);
    }

    private void Update()
    {
        if (time_pause)
            return;

        time_now += Time.deltaTime;

        if(time_now >= ratio)
        {
            time_now = 0;
            TimeFlow();
        }
    }

    private void Awake()
    {
        time_now = 0f;

        if(Load())
        {

        }
        else
        {
            timeData = new TimeData();
            timeData.InitData();

            // 초기값
            /*
            foreach(int num in timeData.weather)
            {
                Debug.Log(num.ToString());
            }
            */
            UIControl.Instance.SetTime(timeData);
        }

        // Weather Test
        /*
        string str = "";
        int before = 0;

        for (int i = 0; i < 10; i++)
        {
            str = "";
            timeData.rainy_count = 20;

            for (int j = 0; j < 20; j++)
            {
                if (j == 0)
                    str += "0";
                else
                {
                    before = timeData.SetWeather(before);
                    str += before.ToString();
                }                    

                str += "(" + timeData.rainy_count.ToString() + ")";

                if(j < 19)
                    str += ", ";
            }

            Debug.Log(i.ToString() + ": " + str);
            
        }*/
    }

    public void Save()
    {
        Debug.Log("Save Start: " + transform.name);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("Assets/Data/Generic/Time.dat");
        bf.Serialize(file, timeData);
        file.Close();

        Debug.Log("Save Complete");
    }

    public bool Load()
    {
        Debug.Log("Load Start:: Time.dat");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open("Assets/Data/Generic/Time.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            TimeData data = (TimeData)bf.Deserialize(file);

            timeData = data;

            file.Close();
            Debug.Log("Load Complete");
            return true;
        }
        else
        {

            Debug.Log("Load Fail");

            if (file == null)
                Debug.Log("File is null");

            if (file.Length <= 0)
                Debug.Log("File is wlong");

            file.Close();
            return false;
        }
    }
}
