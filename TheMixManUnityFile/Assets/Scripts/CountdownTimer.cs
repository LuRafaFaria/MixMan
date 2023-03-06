using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f, startingTime = 20f;
    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 0)
            currentTime = 0;

        if(currentTime <= 10)
            countdownText.color = Color.yellow;

        if(currentTime <= 5)
            countdownText.color = Color.red;
    }
}
