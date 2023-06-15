using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_chenge : MonoBehaviour
{
    public Text text;
    int days;
    public static bool a_flag;
    float time;
    float a_color;

    void Start()
    {
        // 日数を変化させる
        days = Scene_move.getDay();
        text.text = days + "日目";
        a_color = 1;
        a_flag = true;//a_flagがtrueの間実行する
        time = 1;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            if (a_flag) 
            {
                //Debug.Log(a_color);
                //テキストの透明度を変更する
                text.color = new Color (1, 0.5f, 0.2f, a_color);
                a_color -=  Time.deltaTime;
                //透明度が0になったら終了する。
                if (a_color < 0)
                {
                    a_color = 0;
                    text.color = new Color (1, 0.5f, 0.2f, a_color);
                    a_flag = false;
                }
            }
        }
    }
    
    public static bool getAflag()
    {
        return a_flag;
    }
}
