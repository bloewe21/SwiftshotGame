using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    // static public string[] bestTimes = {"", "", "", "", "", "", "", ""};
    //public bool timerIsRunning = true;
    public Text timeText;
    //public float roundedTime = 0f;
    private float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //roundedTime = Mathf.Round(timePassed * 10.0f) * 0.1f;
        timePassed += Time.deltaTime;
        DisplayTime(timePassed);
    }



    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 99;
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
        //timeText.text = string.Format("{0:00}:{1:00}", seconds, milliSeconds);
    }
}
