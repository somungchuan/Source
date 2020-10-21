using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    private float currentTime;
    private weatherMode currentWeather;
    private dayMode currentDay;
    private float dayTime;
    private float dayTimer;
    private float nightTime;
    private float nightTimer;
    private float weatherTime;
    private float weatherTimer;
    private float sunnyProbability;

    int i = 1;

    public weatherMode CurrentWeather { get => currentWeather; set => currentWeather = value; }
    public dayMode CurrentDay { get => currentDay; set => currentDay = value; }

    public enum weatherMode
    {
        Sunny,
        Rain
    }

    public enum dayMode
    {
        Day,
        Night
    }

    private void Start()
    {
        Initial();
    }
    private void Update()
    {
        TimeUpdate();

        // 昼夜循环
        DayStateUpdate();

        // 天气循环
        WeatherStateUpdate();

        PrintState();
    }

    private void Initial()
    {
        currentTime = 0;

        weatherTime = 7;
        weatherTimer = weatherTime;
        sunnyProbability = 0.3f;

        dayTime = 10;
        nightTime = 10;
        dayTimer = dayTime;
        nightTimer = nightTime;
    }

    private void TimeUpdate()
    {
        currentTime += Time.deltaTime;
        dayTimer -= Time.deltaTime;
        nightTimer -= Time.deltaTime;
        weatherTimer -= Time.deltaTime;
    }

    private void DayStateUpdate()
    {
        if(CurrentDay == dayMode.Day && dayTimer <= 0)
        {
            CurrentDay = dayMode.Night;
            nightTimer = nightTime;
        }

        if(CurrentDay == dayMode.Night && nightTimer <= 0)
        {
            CurrentDay = dayMode.Day;
            dayTimer = dayTime;
        }
    }

    private void WeatherStateUpdate()
    {
        if(weatherTimer <= 0)
        {
            weatherTimer = weatherTime;
            float randomNum = Random.Range(1, 10);
            if(randomNum/10f <= sunnyProbability)
            {
                CurrentWeather = weatherMode.Sunny;
            }
            else
            {
                CurrentWeather = weatherMode.Rain;
            }
        }
    }

    private void PrintState()
    {
        
        if(currentTime > i)
        {
            Debug.Log($"CurrentDay:{currentDay};      CurrentWeather:{currentWeather};");
            i++;
        }
    }
}
