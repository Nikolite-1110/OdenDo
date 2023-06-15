using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject CleraPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] Text Datatext;
    [SerializeField] Text SceneText;
    float timerLimit = 60; // 制限時間
    public static float second = 0f;
    bool timeflag = false;
    bool GameOverFlag = false;
    bool musicChangeFlag = true;
    float timer;
    public static int kyakuCount;
    public static bool gameStopFlag;

    //AudoClipの配列、clipsを宣言します。
    public AudioClip[] clips;
    //AudioSource型の変数audiosを宣言します。
    AudioSource audios;

    void Start()
    {
        audios = GetComponent<AudioSource>();
        kyakuCount = 0;
        image = GetComponent<Image>();
        GameOverFlag = false;
        gameStopFlag = true;
        audios.clip = clips[0];
        audios.Play();
    }

    void Update()
    {
        if(Scene_move.day > 0)
        {
            bool UIflag = text_chenge.getAflag();
            if(UIflag == false)
            {
                timeflag = true;
            }
            

            if(image.fillAmount < 0.25f)
            {
                image.color = Color.red;
                if(musicChangeFlag == true)
                {
                    musicChange();
                }
            }
            else if(image.fillAmount < 0.5f)
            {
                image.color = new Color(1f, 0.8f, 0f);
            }
            else
            {
                image.color = Color.green;
            }

            if(timeflag == true)
            {
                if(PausePanel.activeSelf == false)
                {
                    second += Time.deltaTime;
                    // 時間を加算
                    //Debug.Log("second:" + second);
                    timer = second / timerLimit;
                    // 0～1の値に変換
                    Debug.Log("timer:" + timer);
                    image.fillAmount =1 - timer;
                    if(timer > 1)
                    {
                        gameStopFlag = false;
                        timeflag = false;
                        if(GameOverFlag == false)
                        {
                            if(ScoreScript.score < ScoreScript.targetScore)
                            {
                                // ゲームオーバーの画面移動
                                GameOverFlag = true;
                                FadeManager.Instance.LoadScene("Over_Scene", 0.3f);
                            }

                            else
                            {
                                if(Scene_move.day >= 3)
                                {
                                    SceneText.text = "クリア画面へ　 Click!!";
                                }
                                Debug.Log("yamada");
                                int KYAKU = kyakuCount;
                                int SCORE = ScoreScript.score * 100 + KYAKU * 1000;
                                CleraPanel.SetActive(true);
                                Datatext.text = "売上：" + ScoreScript.score + "\n満足した客の数：" + KYAKU + "\nSCORE：" + SCORE;
                                CleraPanel.transform.SetAsLastSibling();
                            }
                        }
                    }  
                }
            }
        }
    }

    public static void kyakuCountMethod()
    {
        kyakuCount += 1;
    }

    public void musicChange()
    {
        audios.clip = clips[1];
        audios.Play();
        musicChangeFlag = false;
    }

    public void addTime(int add)
    {
        second += add;
    }
        
}
