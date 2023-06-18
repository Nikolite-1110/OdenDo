using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_move : MonoBehaviour
{
    public PanelChenge PanelChenge;
    public static int day = 0;
    bool startFlag = false;
    AudioSource audioSource;
    public AudioClip Taiko;
    List<int> ResetList = new List<int>() {1, 4, 7, 10, 13, 16};

    void Start()
    {
        Application.targetFrameRate = 60;
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            day = 0;
            TimerScript.gameStopFlag = true;
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    public void GameStart()
    {
        

        if(PanelChenge.buyFlag == false)
        {
            day = day + 1;
            TimerScript.second = 0f;
            FadeManager.Instance.LoadScene("Game_Scene", 0.3f);
            audioSource.PlayOneShot(Taiko);

            
        }

        if(startFlag == true)
        {
            ItemSelector.ItemList = PanelChenge.kaesikaesi();
        }
        startFlag = true;
    }

    public static int getDay()
    {
        return day;
    }

    public void shop()
    {
        if(day >= 3)
        {
            FadeManager.Instance.LoadScene("Clear_Scene", 0.3f);
        }
        else
        {
            audioSource.PlayOneShot(Taiko);
            FadeManager.Instance.LoadScene("Shop_Scene", 0.3f); 
        }
    }

    public void TutorialMove()
    {
        audioSource.PlayOneShot(Taiko);
        FadeManager.Instance.LoadScene("tutorial", 0.3f);
    }

    public void BackTitle()
    {
        day = 0;
        startFlag = false;
        ItemSelector.ItemList = ResetList;
        FadeManager.Instance.LoadScene("Title", 0.3f);
        audioSource.PlayOneShot(Taiko);
        // ゲームオーバーからタイトルに戻る
    }
}
