using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelChenge : MonoBehaviour
{
    [SerializeField]GameObject buyPanel;
    [SerializeField]GameObject Upgradepanel;
    [SerializeField]GameObject buyItempanel;
    [SerializeField]Text moneytext;
    [SerializeField]GameObject Button;
    [SerializeField]GameObject NedanPanel;
    [SerializeField]Text NedanText;
    public shop_kanri shop_kanri;
    GenerateCard GenerateCard;
    private Button nakami;
    public int Money;
    public static bool buyFlag = false;
    public Transform Parent;
    public Transform ItemParent;
    int result, checknum, checknum2, checknum3;
    
    public static List<int> myListPC;

    void Start()
    { 
        myListPC = ItemSelector.hairetukaesu();
        GenerateCard = Button.GetComponent<GenerateCard>();
        GameObject[] Card = new GameObject[6];
        GameObject[] BuyCard = new GameObject[3];
        int[] daburi = {0, 0, 0};
        //Debug.Log("yes");
        
        Debug.Log(string.Join(", ",myListPC));
        
        
        

        //　カードボタンの生成（アイテム購入）
        for(int k = 0; k < 3; k++)
        {

            // ダブり防止
            do{
                GenerateCard.GenerateID = (int)UnityEngine.Random.Range (1.0f, 33.0f);
                if(GenerateCard.GenerateID % 3 == 0)
                {
                    GenerateCard.GenerateID -= 2;
                }
                else if(GenerateCard.GenerateID % 3 == 2)
                {
                    GenerateCard.GenerateID -= 1;
                }
                result = Array.IndexOf(daburi, GenerateCard.GenerateID);
                daburi[k] = GenerateCard.GenerateID;
                Debug.Log(k + "result:" + result);

                checknum = myListPC.IndexOf(GenerateCard.GenerateID);
                checknum2 = myListPC.IndexOf(GenerateCard.GenerateID + 1);
                checknum3 = myListPC.IndexOf(GenerateCard.GenerateID + 2);
            }while(result >= 0 || checknum >= 0 || checknum2 >= 0 || checknum3 >= 0);
    
            BuyCard[k] = (GameObject)Instantiate(Button, new Vector3(0,0,0), Quaternion.identity);
            BuyCard[k].transform.SetParent(ItemParent);
            BuyCard[k].transform.localScale = new Vector3(3f,3f,3f);
            BuyCard[k].name = "BuyCard"+ k;
            nakami = BuyCard[k].GetComponent<Button>();
            nakami.onClick.AddListener(shop_kanri.kounyuButton);
        }
        
        // カードボタンの生成（入れ替え・アップグレード）
        for(int i = 0; i < 6; i++)
        {
            GenerateCard.GenerateID = myListPC[i];
            Card[i] = (GameObject)Instantiate(Button, new Vector3(0,0,0), Quaternion.identity);
            Card[i].transform.SetParent(Parent);
            Card[i].transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            Card[i].name = "" + i;
            //Debug.Log("seiseiさん");
            nakami = Card[i].GetComponent<Button>();
            nakami.onClick.AddListener(shop_kanri.UpgradeButton);
        }

        Card[0].transform.localPosition = new Vector3(-600,120,0);
        Card[1].transform.localPosition = new Vector3(0,120,0);
        Card[2].transform.localPosition = new Vector3(600,120,0);
        Card[3].transform.localPosition = new Vector3(-600,-240,0);
        Card[4].transform.localPosition = new Vector3(0,-240,0);
        Card[5].transform.localPosition = new Vector3(600,-240,0);

        BuyCard[0].transform.localPosition = new Vector3(-540, 0, 0);
        BuyCard[1].transform.localPosition = new Vector3(0, 0, 0);
        BuyCard[2].transform.localPosition = new Vector3(540, 0, 0);
        
        //Debug.Log("no");

        Debug.Log("前金：" + Money);
        Money = Money + ScoreScript.Tellscore();
        Debug.Log("現在のお金：" + Money);

    }

    
    void Update()
    {
        moneytext.text = "所持金：" + Money + "円";
    }

    public void chengePanel()
    {
        if(buyFlag == false)
        {
            if(buyPanel.activeSelf == true)
            {
                buyPanel.SetActive(false);
                Upgradepanel.SetActive(true);
                buyItempanel.SetActive(false);
            }

            else if(Upgradepanel.activeSelf == true)
            {
                buyPanel.SetActive(true);
                buyItempanel.SetActive(true);
                Upgradepanel.SetActive(false);
            }
        }
    }

    public static List<int> kaesikaesi()
    {
        return myListPC;
    }

    public void ryoukin()
    {
        if(NedanPanel.activeSelf == true)
        {
            buyFlag = false;
            NedanPanel.SetActive(false);
            NedanText.text = "料金表";
            if(buyPanel.activeSelf == true)
            {
                buyItempanel.SetActive(true);
            }
            else
            {
                Upgradepanel.SetActive(true);
            }
        }
        else
        {
            Upgradepanel.SetActive(false);
            buyFlag = true;
            buyItempanel.SetActive(false);
            NedanPanel.SetActive(true);
            NedanText.text = "閉じる";
        }
    }

}
