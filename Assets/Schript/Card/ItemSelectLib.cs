using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ItemSelect
{
    public int[] ItemErea = new int[4] {0, 0, 0, 0};  //手札にあるカードのアイテムIDを入れておくリストです。
    public List<int> SelectItem = new List<int>(); //選択したカードのインデックス番号を入れておくリストです。
    public List<int> ItemList = new List<int>(); //アイテムIDを入れておくリストです。

    public List<int> SelectItemData = new List<int> (); //選択したアイテムのIDを格納します。
    int SatietyValue; //処理する際に使用する満足値を格納します。
    bool CheckItem = true; //アイテムの重複を防止するために使用します。
    bool DoTwice; //餅巾着の効果用に使用します。
    double XTime; //満腹値の増加倍率を格納します。

    

    //手札の空いている場所にカードを生成します。
    public void ItemGenerator()
    {
        for(int i = 0; i < 4; i++){
            int GeneratItemNamber;

            if (ItemErea[i] == 0){
                do {
                    GeneratItemNamber = Random.Range(0, ItemList.Count);
                    ItemErea[i] = ItemList[GeneratItemNamber];
                    if(ItemErea.Count(n => n == ItemList[GeneratItemNamber]) < 3){
                        CheckItem = false;
                    }
                } while(CheckItem);
                
            }
        }

        SelectItem.Clear();

        ItemEreaOutput();
    }
    
    //手札１番目のカードを選択します
    public void SetIndex0(){
        if(SelectItem.Contains(0)){
                SelectItem.Remove(0);
        } else {
            if(SelectItem.Count < 2){
                SelectItem.Add(0);
            } else {
                Debug.Log("アイテムの選択は2つまでです。");
            }
        }

        SelectItemOutput();
    }

     //手札2番目のカードを選択します
    public void SetIndex1(){
        if(SelectItem.Contains(1)){
            SelectItem.Remove(1);
        } else {
            if(SelectItem.Count < 2){
                SelectItem.Add(1);
            } else {
                Debug.Log("アイテムの選択は2つまでです。");
            }
        }

        SelectItemOutput();
    }

     //手札3番目のカードを選択します
    public void SetIndex2(){
        if(SelectItem.Contains(2)){
            SelectItem.Remove(2);
        } else {
            if(SelectItem.Count < 2){
                SelectItem.Add(2);
            } else {
                Debug.Log("アイテムの選択は2つまでです。");
            }
        }

        SelectItemOutput();
    }

     //手札4番目のカードを選択します
    public void SetIndex3(){
        if(SelectItem.Contains(3)){
            SelectItem.Remove(3);
        } else {
            if(SelectItem.Count < 2){
                SelectItem.Add(3);
            } else {
                Debug.Log("アイテムの選択は2つまでです。");
            }
        }

        SelectItemOutput();
    }

    //カードを場に出す処理です。
    public void PlayACard(){
        if(SelectItem.Count != 0) {
            for(int i = 0; i < SelectItem.Count; i++){
                SelectItemData[i] = ItemErea[SelectItem[i]];
                ItemErea[SelectItem[i]] = 0;
            }

            ItemGenerator();
        } else {
            Debug.Log("カードを選択していません。");
        }
    }

    
    //選択したアイテムのインデックス番号を表示します（デバッグ用）
    public void SelectItemOutput()
    {
        Debug.Log("選択したアイテム:" + string.Join(", ", SelectItem.Select(n => n.ToString())));
    }

    //どういった動作ができるか表示します（デバッグ用）
    public void ItemEreaOutput()
    {
        Debug.Log("[" + ItemErea[0] + "]" + "[" + ItemErea[1] + "]" + "[" + ItemErea[2] + "]" + "[" + ItemErea[3] + "]");
        Debug.Log("1~4キー:カードの選択  Space:カードを場に出す");
    }

    

    //満腹値の判定を行います。(引数アイテムID）
    public int SetietyValue(List<int> list){
        double Sum = 0;
        int Result;

        //各値を無理やり足してます。なにか効率的な書き方があれば教えてください。
        for(int i = 0; i < list.Count; i++){
            switch (list[i]){
                case 10:
                case 13:
                case 16:
                    Sum += 1;
                    break;
                    
                case 11:
                case 14:
                case 17:
                case 28:
                case 31:
                    Sum += 2;
                    break;

                case 12:
                case 19:
                case 29:
                case 32:
                    Sum += 3;
                    break;

                case 1:
                case 15:
                case 20:
                    Sum += 4;
                    break;

                case 4:
                case 7:
                case 18:
                case 21:
                    Sum += 5;
                    break;

                case 2:
                    Sum += 6;
                    break;

                case 5:
                case 8:
                    Sum += 7;
                    break;

                case 3:
                    Sum += 8;
                    break;

                case 6:
                case 9:
                    Sum += 9;
                    break;

                case 30:
                case 33:
                    Sum += 12;
                    break;

            }
        }

        if (DoTwice) {
            Sum *= 1.25 + 0.25 * XTime;

            DoTwice = false;
        }

        if (list.Contains(22) || list.Contains(23) || list.Contains(24)) {
            DoTwice = true;
            XTime = list.Find(n => n >= 22 && n <= 24);

            XTime %= 3;
            XTime ++;
        }

        Result = (int)Sum;

        return Result;
    }
}
