using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text scoretext;
    public static int score = 0;
    public static int targetScore = 0;
    float fillscore = 0;
    bool scoreflag = false;
    int scorePer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        image.color = new Color(0f, 0.75f, 1f);
        image.fillAmount = 0;
        scoreflag = true;
        score = 0;
        if(Scene_move.day == 1) // １日目の目標スコア設定
        {
            targetScore = 75;
        }
        else if(Scene_move.day == 2) // ２日目の目標スコア設定
        {
            targetScore = 150;
        }
        else if(Scene_move.day == 3) // ３日目の目標スコア設定
        {
            targetScore = 235;
        }
        else
        {
            targetScore = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(score);
        //Debug.Log(targetScore);
        //Debug.Log(fillscore);

        // スコアに応じてゲージが伸びる
        if(scoreflag == true)
        {
            fillscore = (float)score / (float)targetScore;
            image.fillAmount = fillscore;
        }
        
        // スコアが超えたら色が変わる
        if(score >= targetScore)
        {
            image.color = new Color(1f, 0.1f, 0f);
            scoreflag = false;
        }

        
        scoretext.text = (fillscore*100).ToString("000") + "%";
    }

    public static int Tellscore()
    {
        return score;
    }

    public void test()
    {
        score += 10;
    }
}
