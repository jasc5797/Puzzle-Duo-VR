using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float timeRemaining;
    public float startingTime = 300;
    //float timeRemainingReset = 300;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = startingTime ;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        int seconds = (int)(timeRemaining % 60);
        int minutes = (int)(timeRemaining/ 60) % 60;
        //int hours = (int)(timeRemaining / 3600) % 24;

        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

        //countdownText.text = timeRemaining.ToString("00");
        countdownText.text = timerString;

        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            //timeRemaining = timeRemainingReset;
        }

    }
}
