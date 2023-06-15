using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageTake : MonoBehaviour
{
    public berController BerController;
    public GameObject effect;
    GameObject canvas;
    List<int> Pnolist = new List<int>();

    public int DT_damage;
    public List<string> tmp = new List<string>();

    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }


    void Update()
    {
        //デバッグ用
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.Attack(0, 4, new List<int>() {10}, new List<string>());
        }
        */
    }

    public void Attack (int DHPos, int Damage ,List<int> pattern, List<string> color)
    {
        tmp = color;
        for(int i = 0; i < tmp.Count; i++){
            Debug.Log(tmp[i]);
        }
        
        float posx =(DHPos * 149) - 747;
        Vector2 Postion = new Vector2(posx, -471);
        DT_damage = Damage;
        GameObject go1 = Instantiate(effect, canvas.transform) as GameObject;
        go1.GetComponent<RectTransform>().anchoredPosition = Postion;

        effectController MoveType = go1.GetComponent<effectController>();
        MoveType.muki = 0;
        MoveType.setColor = tmp;
        for(int i = 0; i < tmp.Count; i++){
            Debug.Log(tmp[i]);
        }
       // Debug.Log(Postion.x + "." + Postion.y);

        for(int i = 0; i < pattern.Count; i++){
            switch (pattern[i])
            {
                //爆発
                case 10:
                    Pnolist.Add(2);
                    MoveType.doExplosionMove = 1;
                    break;
                case 11:
                    Pnolist.Add(2);
                    MoveType.doExplosionMove = 2;
                    break;
                case 12:
                    Pnolist.Add(2);
                    MoveType.doExplosionMove = 3;
                    break;

                //縦貫通
                case 13:
                    Pnolist.Add(3);
                    break;
                case 14:
                    Pnolist.Add(4);
                    break;
                case 15:
                    Pnolist.Add(5);
                    break;
                
                //横拡散
                case 16:
                    MoveType.doHolizonMove = 1;
                    Pnolist.Add(1);
                    break;
                case 17:
                    MoveType.doHolizonMove = 2;
                    Pnolist.Add(2);
                    break;
                case 18:
                    MoveType.doHolizonMove = 3;
                    Pnolist.Add(2);
                    break;

                //回復
                case 25:
                    MoveType.doHealTime = 1;
                    Pnolist.Add(1);
                    break;
                case 26:
                    MoveType.doHealTime = 2;
                    Pnolist.Add(1);
                    break;
                case 27:
                    MoveType.doHealTime = 3;
                    Pnolist.Add(1);
                    break;

                default:
                    Pnolist.Add(1);
                    break;
            }
        }
        MoveType.Pno = Pnolist.Max();   
        Pnolist.Clear();
    }  
}
