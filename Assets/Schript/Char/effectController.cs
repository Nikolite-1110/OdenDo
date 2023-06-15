using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectController : MonoBehaviour
{
    public int Pno; //貫通数
    public int muki = 0; //向き 0=縦、1=左、2=右 3=斜め左 4=斜め右
    public List<string> setColor = new List<string>();

    public GameObject effectPrefab;
    GameObject canvas;

    int EC_pattern;
    int EC_damage;

    public List<string> touchChar = new List<string>(); //接触したオブジェクトの名前を保管する。

    int count; //横列等は複製された最初の1回はカウントしない

    public  Vector3 MovingDamage;
    DamageTake damageTake;

    //以下特殊動作用変数(貫通・倍化動作を除く)
    public int doHolizonMove = 0;
    public int doExplosionMove = 0;
    public int doHealTime = 0;


    

    void Start()
    {
        GameObject generateEffect = GameObject.Find("under");
        canvas = GameObject.Find("Canvas");
        damageTake = generateEffect.GetComponent<DamageTake>();

        MovingDamage = gameObject.GetComponent<RectTransform>().anchoredPosition;
        EC_damage = damageTake.DT_damage;
        setColor = damageTake.tmp;

        
    }

    void Update()
    {
        switch(muki){
            case 0:
                MovingDamage.y += 10;
                break;
            case 1:
                MovingDamage.x -= 4;
                break;
            case 2:
                MovingDamage.x += 4;
                break;
            case 3:
                MovingDamage.x -= 4;
                MovingDamage.y += 8;
                break;
            case 4:
                MovingDamage.x += 4;
                MovingDamage.y += 8;
                break;
        }
        
        gameObject.GetComponent<RectTransform>().anchoredPosition = MovingDamage;

        if(MovingDamage.x < -830 || MovingDamage.x > -360 || MovingDamage.y > 420){
            Debug.Log("aaa");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "End"){
            Debug.Log("aaa");
            Destroy(gameObject);
        }

        GameObject hitChar = other.gameObject;

        //Debug.Log(other.name + "に当たったよ");

        if (other.tag == "Char" || other.tag == "CharTest")
        {   
            if(!touchChar.Contains(other.name)){
                //Debug.Log("接触");
                //Debug.Log(hitChar.name);
                
                //接触時、ダメージの受け渡し＋貫通可能数-1

                other.gameObject.GetComponent<berController>().HPdamage(EC_damage, setColor);
                touchChar.Add(other.name);

                if (doHolizonMove != 0){
                    HolizonMove(MovingDamage);
                }

                if (doExplosionMove != 0){
                    ExplosionMove(MovingDamage, doExplosionMove);
                    doExplosionMove = 0;
                }

                if (doHealTime != 0){
                    other.gameObject.GetComponent<berController>().HPhealing(doHealTime);
                }

                Pno -= 1;

                //貫通可能数が0になったら消去
                if(Pno <= 0)
                {
                    Destroy(this.gameObject);
                    //Debug.Log("destroy");
                }
            }   
        }
    }

    public void HolizonMove (Vector3 effectPosition)
    {
        GameObject go2 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go2.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go2.GetComponent<effectController>().muki = 1;
        go2.GetComponent<effectController>().Pno = 1;
        go2.GetComponent<effectController>().doHolizonMove = 0;
        go2.name = "hidarimuki";

        GameObject go3 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go3.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go3.GetComponent<effectController>().muki = 2;
        go3.GetComponent<effectController>().Pno = 1;
        go3.GetComponent<effectController>().doHolizonMove = 0;
        go3.name = "migimuki";
        
        //Debug.Log("複製");

    }

    public void ExplosionMove (Vector3 effectPosition, int level)
    {
        GameObject go4 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go4.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go4.GetComponent<effectController>().muki = 1;
        go4.GetComponent<effectController>().Pno = 1;
        go4.GetComponent<effectController>().doExplosionMove = 0;
        go4.name = "hidarimuki";

        GameObject go5 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go5.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go5.GetComponent<effectController>().muki = 2;
        go5.GetComponent<effectController>().Pno = 1;
        go5.GetComponent<effectController>().doExplosionMove = 0;
        go5.name = "migimuki";

        GameObject go6 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go6.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go6.GetComponent<effectController>().muki = 3;
        go6.GetComponent<effectController>().Pno = 1;
        go6.GetComponent<effectController>().doExplosionMove = 0;
        go6.name = "nanamehidari";

        GameObject go7 = Instantiate(effectPrefab, canvas.transform) as GameObject;
        go7.GetComponent<RectTransform>().anchoredPosition = effectPosition;
        go7.GetComponent<effectController>().muki = 4;
        go7.GetComponent<effectController>().Pno = 1;
        go7.GetComponent<effectController>().doExplosionMove = 0;
        go7.name = "nanamemigi";
    }
}
