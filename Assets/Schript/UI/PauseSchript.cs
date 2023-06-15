using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSchript : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    float StopTime = 0f;
    void Start()
    {
        StopTime = 0f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            PausePanel.SetActive(true);
            StopTime = TimerScript.second;
        }
    }
    
    public void ClosePanel()
    {
        PausePanel.SetActive(false);
        TimerScript.second = StopTime;
    }
}
