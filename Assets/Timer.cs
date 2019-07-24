using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = PlayField.totalTime;

    }
    private void loadTexture(int timeInSeconds)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Time.time);
        //timerText.text = Time.time.ToString();
        float t = startTime - Time.time;
        int minutes = ((int)t / 60);
        string seconds = (t % 60).ToString("f0");

        
        timerText.text = minutes + ":" + seconds;

    }
}
