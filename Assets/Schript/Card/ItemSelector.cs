using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemSelector : MonoBehaviour
{
    ///////////////////////////
    //アニメーション調整用変数//
    ///////////////////////////

    //器のアニメーション
    float fadeTime = 0.3f;  //フェードにかかる時間
    Vector3 Move = new Vector3(1, 0, 0);  //毎フレーム移動する距離
    float timeDifference = 0.5f;  //器の生成をしなおす際の時間のずれ(詳しくは、コルーチン部分参照)

    ////////////////////////////

    public static List<int> ItemList = new List<int>(){1, 4, 7, 10, 13, 16}; //アイテムIDを入れておくリストです。
    int[] ItemErea = new int[4] {0, 0, 0, 0};  //手札にあるカードのアイテムIDを入れておくリストです。

    public List<int> SelectItem = new List<int>();//選択したカードのインデックス番号を入れておくリストです。
    public List<int> SelectItemData = new List<int>() ;//選択したアイテムのIDを格納します。

    int[] GuzaiList = new int[2] {-1, -1};//具材の持つインデックス番号を入れておくリストです。

    public GameObject CardPrefab;
    public GameObject GuzaiPrefab;
    public GameObject UtsuwaPrefab;

    GameObject Canvas;
    Transform CanvasTransform;

    GameObject Guzai1;
    GameObject Guzai2;
    GameObject Utsuwa;

    ObjectMoving UtuwaMoving;

    bool DoTwice; //餅巾着の効果用に使用します。
    double XTime; //満腹値の増加倍率を格納します。

    public int ProvisionLocation; //どの場所から提出するかを格納します(A = 0, S = 1, D = 2)
    public int UsingSatietyValue; //満足値を格納します。
    public List<string> UsingColor = new List<string>();//提出する時の色を格納します。
    public List<int> SetItemID = new List<int>(); //提出するカードのID番号を格納します。

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        Canvas = GameObject.Find("Canvas");

        CanvasTransform = Canvas.GetComponent<Transform>();
        
        
        ItemGenerator();
        ItemEreaOutput();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerScript.gameStopFlag == true)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
                if (Input.GetKeyDown(KeyCode.A)){
                    ProvisionLocation = 0;
                } else if (Input.GetKeyDown(KeyCode.S)){
                    ProvisionLocation = 1;
                } else if (Input.GetKeyDown(KeyCode.D)){
                    ProvisionLocation = 2;
                }
                PlayACard();
            }
        }

        if(Input.GetKeyDown(KeyCode.H)){
            SetGuzai(1, 1);
        }
    }

    //手札の空いている場所にカードを生成します。
    void ItemGenerator() {
        int GeneratItemNamber;

        for (int i = 0; i < 4; i++){    

            if(ItemErea[i] == 0){
                do {
                    GeneratItemNamber = Random.Range(0, ItemList.Count);
                    ItemErea[i] = ItemList[GeneratItemNamber];
                } while(ItemErea.Count(n => n == ItemList[GeneratItemNamber]) >= 2); 
            }
            
            GameObject Card = Instantiate(CardPrefab, Vector3.zero, Quaternion.identity, CanvasTransform) as GameObject;
            Card.transform.SetParent(CanvasTransform);
            Card.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
            Card.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200 + i * 300, -352, 0.0f);  

            Card.GetComponent<GenerateCard>().GenerateID = ItemErea[i];

            int j = i;
            Card.GetComponent<Button>().onClick.AddListener(() => {SetIndex(j);});
  
            string CardName = "Card_" + i.ToString();
            Card.name = CardName;
        }

        GenerateUtsuwa();

        SelectItem.Clear();
        SelectItemData.Clear();

        GuzaiList[0] = -1;
        GuzaiList[1] = -1;

        ItemEreaOutput();
    }

    
    //手札のカードを選択します
    public void SetIndex(int n){
        if(SelectItem.Contains(n)){
                SelectItem.Remove(n);
                DestroyGuzai(n);
        } else {
            if(SelectItem.Count < 2){
                SelectItem.Add(n);
                SetGuzai(n, ItemErea[n]);
            } else {
                Debug.Log("アイテムの選択は2つまでです。");
            }
        }

        SelectItemOutput();
    }


    //カードを場に出す処理です。
    public void PlayACard(){
        //Debug.Log("SelectItem.Count:" + SelectItem.Count);

        if(SelectItem.Count != 0) {
            for(int i = 0; i < SelectItem.Count; i++){
                SelectItemData.Add(ItemErea[SelectItem[i]]);
                ItemErea[SelectItem[i]] = 0;
            }

            SetItemID = SelectItemData;
            UsingSatietyValue = SatietyValue(SelectItemData);
            SetColor(SelectItemData);
                
            for(int i = 0; i < 4; i++){
                GameObject DestroyGameObject = GameObject.Find("Card_" + i.ToString());
                Destroy(DestroyGameObject);
            }

            UtuwaMoving.fadeErase(new Vector3(-1, 0, 0), fadeTime);


            GameObject under = GameObject.Find("under");
            DamageTake doPlay = under.GetComponent<DamageTake>();
            for(int i = 0; i < UsingColor.Count; i++){
                Debug.Log(UsingColor[i]);
            }
            doPlay.Attack(ProvisionLocation, UsingSatietyValue, SetItemID, SetColor(SelectItemData));

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

    //器の生成（コルーチンを使う関係で、別関数に分ける）
    void GenerateUtsuwa(){
        StartCoroutine("SetUtsuwa");
    }

    //器生成のコルーチン
    IEnumerator SetUtsuwa(){
        //器の生成
        yield return new WaitForSeconds(timeDifference);

        Utsuwa = Instantiate(UtsuwaPrefab, Vector3.zero, Quaternion.identity, CanvasTransform) as GameObject;
        Utsuwa.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 74, 0);
        UtuwaMoving = Utsuwa.GetComponent<ObjectMoving>();
        UtuwaMoving.fadeGenerate(new Vector3(-1, 0, 0), fadeTime);
    }


    //選択した具材を器に載せる。
    public void SetGuzai(int indexNumber, int IDNumber)
    {
        Transform UtsuwaTransform = Utsuwa.GetComponent<Transform>();

        if(GuzaiList[0] < 0) {
            Guzai1 = Instantiate(GuzaiPrefab, Vector3.zero, Quaternion.identity, UtsuwaTransform) as GameObject;
            Guzai1.transform.SetParent(UtsuwaTransform);
            Guzai1.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
            Guzai1.name = "GuzaiA";
            Guzai1.GetComponent<GuzaiController>().GuzaiNum = indexNumber;
            Guzai1.GetComponent<GuzaiController>().GuzaiID = IDNumber;
            Guzai1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-20, 20, 0);

            GuzaiList[0] = indexNumber;

        } else if (GuzaiList[1] < 0) {
            Guzai2 = Instantiate(GuzaiPrefab, Vector3.zero, Quaternion.identity, UtsuwaTransform) as GameObject;
            Guzai2.transform.SetParent(UtsuwaTransform);
            Guzai2.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
            Guzai2.name = "GuzaiB";
            Guzai2.GetComponent<GuzaiController>().GuzaiNum = indexNumber;
            Guzai2.GetComponent<GuzaiController>().GuzaiID = IDNumber;
            Guzai2.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 20, 0);

            GuzaiList[1] = indexNumber;
        }
    }

    //既に選択した具材を解除するときにオブジェクトを削除する
    void DestroyGuzai(int DestroyIndexNumber)
    {
        if (GuzaiList[0] == DestroyIndexNumber){
            GuzaiController Guzai1Script = Guzai1.GetComponent<GuzaiController>();
            Guzai1Script.GuzaiDestroy();
            GuzaiList[0] = -1;
        } else if (GuzaiList[1] == DestroyIndexNumber){
            GuzaiController Guzai2Script = Guzai2.GetComponent<GuzaiController>();
            Guzai2Script.GuzaiDestroy();
            GuzaiList[1] = -1;
        }
    }

    //満腹値の判定を行います。(引数アイテムID）
    public int SatietyValue(List<int> list){
        double Sum = 0;
        int Result;

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
                case 25:
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
                case 26:
                    Sum += 4;
                    break;

                case 4:
                case 7:
                case 18:
                case 21:
                    Sum += 5;
                    break;

                case 2:
                case 27:
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

    //色を記録します。
    List<string> SetColor(List<int> list){
        List<string> result = new List<string>();

        for(int i = 0; i < list.Count; i++){
            switch(list[i]){
                case 1:
                case 2:
                case 3:
                case 10:
                case 11:
                case 12:
                case 22:
                case 23:
                case 24:
                case 31:
                case 32:
                case 33:
                    result.Add("green");
                    break;
                case 4:
                case 5:
                case 6:
                case 13:
                case 14:
                case 15:
                case 25:
                case 26:
                case 27:
                    result.Add("red");
                    break;
                case 7:
                case 8:
                case 9:
                case 16:
                case 17:
                case 18:
                case 28:
                case 29:
                case 30:
                    result.Add("blue");
                    break;
                case 19:
                case 20:
                case 21:
                    result.Add("red");
                    result.Add("blue");
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    public static List<int> hairetukaesu()
    {
        return ItemList;
    }
}
