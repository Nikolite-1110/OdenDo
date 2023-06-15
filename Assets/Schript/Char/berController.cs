using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class berController : MonoBehaviour
{
    public GameObject hpbar;
    public GameObject backbar;
    public string color;
    public bool canAngryBack = true;
    public bool canBack = true;

    public charController CharController;
    public charPostion CharPostion;

    public float Mback = 5.0f;
    public float Mhp = 10.0f;

    float delta = 0;
    
    bool doDecrease_hp = false;

    float back_display;

    public float hp = 0;

    void Awake(){
        hp = 0;
    }

    void Start()
    {
        canAngryBack = true;
        canBack = true;
    }

    void Update()
    {
        this.delta += Time.deltaTime;       
        
        if (this.delta > 1)
        {
            this.back_display = delta / Mback;
        }
        
//客が満足して帰る処理        
        if (this.hp >= this.Mhp && canBack)
        {

            //スコアアップ
            CharController.CharCreate(CharPostion.BPos);
            GameObject[] Char = GameObject.FindGameObjectsWithTag("Char");
            foreach (var obj1 in Char)
            {
                charPostion obj1move = gameObject.GetComponent<charPostion>();

                obj1move.charMove(CharPostion.HPos, CharPostion.BPos);
            }

            ScoreScript.score += 5; 
            //Debug.Log("満足");
            ObjectMoving move = gameObject.GetComponent<ObjectMoving>();
            TimerScript.kyakuCountMethod();
            move.fadeErase(new Vector3(-2, 0, 0), 1);
        }

//客が時間で帰る処理
        if (back_display > 1 && canAngryBack)
        {
            //制限時間ダウン
            CharController.CharCreate(CharPostion.BPos);
            GameObject[] Char = GameObject.FindGameObjectsWithTag("Char");
            if(GameObject.Find("Timer") == true)
            {
                GameObject Timer = GameObject.Find("filledcircle");
                TimerScript timerScript = Timer.GetComponent<TimerScript>();
                timerScript.addTime(2);
            }
            

            foreach (GameObject obj2 in Char)
            {
                charPostion obj2move = gameObject.GetComponent<charPostion>();

                obj2move.charMove(CharPostion.HPos, CharPostion.BPos);
            }
            ObjectMoving move = gameObject.GetComponent<ObjectMoving>();
            GetComponent<Image>().color = new Color32 (255, 0, 0, 255);
            move.fadeErase(new Vector3(-2, 0, 0), 1);
            
        }

        this.backbar.GetComponent<Image>().fillAmount = 1 - this.delta / this.Mback;
        this.hpbar.GetComponent<Image>().fillAmount = this.hp / this.Mhp;
    }

    public void HPdamage(int damage, List<string> Checkedcolor)
    {
        int resultDamage = damage;
        int countColor = 0;
        for(int i = 0;i < Checkedcolor.Count; i++){
            if (Checkedcolor[i] == color){
                countColor++;
            }
        }

        switch(countColor){
            case 0:
                break;
            case 1:
                resultDamage = (int)(resultDamage * 1.5f);
                break;
            case 2:
                resultDamage = (int)(resultDamage * 2.0f);
                break;
            default:
                break;
        }
        Debug.Log(resultDamage.ToString() + "ダメージ" );
        hp += resultDamage; 
    }

    public void HPhealing(int level){
        Mback += level * 2 + 2;
    }

    public void changeMaxHp(int Change)
    {
        Mhp = Change;
    }
}
