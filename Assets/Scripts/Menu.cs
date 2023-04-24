using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject timesPanel;
    [SerializeField] GameObject stopwatchPanel;
    [SerializeField] TMP_Text timeObj;

    [SerializeField] TMP_Text[] timesTextObj;

    [SerializeField] TMP_Text winnerText;

    [SerializeField] float currentTime;
    [SerializeField] bool stopwatchActive;
    TimeSpan time;
    List<TimeSpan> times = new List<TimeSpan>();

    [SerializeField] GameObject[] removeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        time = TimeSpan.FromSeconds(currentTime);
        timeObj.text = time.ToString(@"mm\:ss\.fff");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (times.Count == 1)
        {
            removeButton[0].SetActive(true);
        } 
        else if (times.Count == 2)
        {
            removeButton[1].SetActive(true);
        }
        else if (times.Count == 0)
        {
            removeButton[0].SetActive(false);
            removeButton[1].SetActive(false);
        }
    }

    public void StartTimer()
    {
        stopwatchActive = true;
    }

    public void StopTimer()
    {
        stopwatchActive = false;
    }

    public void ResetTimer()
    {
        timeObj.text = "00:00.000";
        currentTime = 0;
    }

    public void SaveTime()
    {
        if (!stopwatchActive) 
        { 
            if (times.Contains(time))
            {
                return;
            }
            times.Add(time);
        }
    }

    public void ViewTimes()
    {
        if (times.Count > 1)
        {
            timesPanel.SetActive(true);
            stopwatchPanel.SetActive(false);
            timesTextObj[0].text = "Group One: \n" + times[0].ToString(@"mm\:ss\:fff");
            timesTextObj[1].text = "Group Two: \n" + times[1].ToString(@"mm\:ss\:fff");

            if (times[0] < times[1])
            {
                winnerText.text = "Group 1 wins!!!";
            }
            else
            {
                winnerText.text = "Group 2 wins!!!";
            }
        }
        else if (times.Count == 1)
        {
            timesPanel.SetActive(true);
            stopwatchPanel.SetActive(false);

            timesTextObj[0].text = "Group One \n" + times[0].ToString(@"mm\:ss\.fff");
        }
        else
        {
            timesPanel.SetActive(true);
            stopwatchPanel.SetActive(false);
            timesTextObj[0].text = "No Saved Times";
        }
    }

    public void AddTime(int timeToAdd)
    {
        currentTime += timeToAdd;
    }

    public void RemoveTime(int timeToRemove)
    {
        if (currentTime - timeToRemove > 0)
        {
            currentTime -= timeToRemove;
        }
        else
        {
            currentTime = 0;
        }
    }

    public void Back()
    {
        timesPanel.SetActive(false);
        stopwatchPanel.SetActive(true);
    }

    public void ResetTimes()
    {
        if (times.Count > 0)
        {
            times.Clear();

            timesTextObj[0].text = "Group One: \n";
            timesTextObj[1].text = "Group Two: \n";
        }
    }

    public void RemoveSaveTime(int Index)
    {
        times.RemoveAt(Index);
        if (Index == 0)
            timesTextObj[Index].text = "Group One: \n";
        if (Index == 1)
            timesTextObj[Index].text = "Group Two: \n";
    }
}
