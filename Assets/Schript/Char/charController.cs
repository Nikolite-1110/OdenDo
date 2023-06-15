using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charController : MonoBehaviour
{
    public float DMback = 30;
    public float DMhp = 12;

    public float charID;

    public GameObject chars;
    GameObject canvas;
    GameObject CharFactory;

    public berController BerController;

    public Image c_Image;
    public Sprite[] c_Sprite;

    bool change = true;

    void Start()
    {
        c_Image = gameObject.GetComponent<Image>();
        CharFactory = GameObject.Find("CharFactory");
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        
    }
    
    public void Charset()
    {
        if (change == true)
        {
            float SPchar = Random.Range(0, 100);
            c_Image = gameObject.GetComponent<Image>();
            
            if (SPchar <= 80)
            {
                float subCharID = Random.Range(0, 12);
                charID = subCharID;
            }
            else
            {
                float subCharID = Random.Range(12, 19);
                charID = subCharID;
            }
            switch (charID)
            {
                case 0:
                    //MenRed
                    c_Image.sprite = c_Sprite[0];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "red";
                    break;

                case 1:
                    //Menblue
                    c_Image.sprite = c_Sprite[1];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "blue";
                    break;

                case 2:
                    //MenGreen
                    c_Image.sprite = c_Sprite[2];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "green";
                    break;

                case 3:
                    //WomenRed
                    c_Image.sprite = c_Sprite[3];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "red";
                    break;

                case 4:
                    //WomenBlue
                    c_Image.sprite = c_Sprite[4];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "blue";
                    break;

                case 5:
                    //WomenGreen
                    c_Image.sprite = c_Sprite[5];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "green";
                    break;

                case 6:
                    //DKred
                    c_Image.sprite = c_Sprite[6];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "red";
                    break;

                case 7:
                    //DKblue
                    c_Image.sprite = c_Sprite[7];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "bule";
                    break;

                case 8:
                    //DKgreen
                    c_Image.sprite = c_Sprite[8];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "green";
                    break;

                case 9:
                    //JKred
                    c_Image.sprite = c_Sprite[9];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "red";
                    break;

                case 10:
                    //JKblue
                    c_Image.sprite = c_Sprite[10];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "blue";
                    break;

                case 11:
                    //JKgreen
                    c_Image.sprite = c_Sprite[11];
                    BerController.Mback = this.DMback;
                    BerController.Mhp = this.DMhp;
                    BerController.color = "green";
                    break;

                case 12:
                    //GataiRed
                    c_Image.sprite = c_Sprite[12];
                    BerController.Mback = DMback;
                    BerController.Mhp = 30;
                    BerController.color = "red";
                    break;

                case 13:
                    //GataiBlue
                    c_Image.sprite = c_Sprite[13];
                    BerController.Mback = DMback;
                    BerController.Mhp = 30;
                    BerController.color = "bule";
                    break;

                case 14:
                    //GataiGreen
                    c_Image.sprite = c_Sprite[14];
                    BerController.Mback = DMback;
                    BerController.Mhp = 30;
                    BerController.color = "green";
                    break;

                case 15:
                    //Mamori
                    c_Image.sprite = c_Sprite[15];
                    BerController.Mback = DMback;
                    BerController.Mhp = 15;
                    BerController.color = "red";
                    break;

                case 16:
                    //SyuhuRed
                    c_Image.sprite = c_Sprite[16];
                    BerController.Mback = 10;
                    BerController.Mback = 8;
                    BerController.color = "red";
                    break;

                case 17:
                    //SyuhuBlue
                    c_Image.sprite = c_Sprite[17];
                    BerController.Mback = 10;
                    BerController.Mhp = 8;
                    BerController.color = "blue";
                    break;

                case 18:
                    //SyuhuGreen
                    c_Image.sprite = c_Sprite[18];
                    BerController.Mback = 10;
                    BerController.Mback = 8;
                    BerController.color = "green";
                    break;

                default:
                    Debug.Log("20");
                    break;
            }
            change = false;
        }
    }

    public void CharCreate(int Bpos)
    {
        float xpos = -747 + (Bpos * 149);
        Vector2 ypos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        Vector2 Postion = new Vector2(xpos, ypos.y + 930f);
        GameObject go = Instantiate(chars, canvas.transform) as GameObject;
        go.GetComponent<RectTransform>().anchoredPosition = Postion;
        charController CharController = go.GetComponent<charController>();
        go.name = "char" + CharFactory.GetComponent<charGenerator>().nameCount;
        CharFactory.GetComponent<charGenerator>().nameCount++; 

        CharController.Charset();
    }    
}
