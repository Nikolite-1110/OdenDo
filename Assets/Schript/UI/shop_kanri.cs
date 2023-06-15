using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class shop_kanri : MonoBehaviour
{
    public Text UPText;

    // パネルの入れ物
    [SerializeField]GameObject MoneyPanel;
    [SerializeField]GameObject buyCheckpanel;
    [SerializeField]GameObject buyItempanel;
    [SerializeField]GameObject Upgradepanel;
    [SerializeField]GameObject buypanel;

    // スクリプトとかボタンの入れ物
    private  EventSystem eventSystem;
    private Button nakami;
    public PanelChenge PanelChenge;
    GameObject ButtonObject;
    GenerateCard GenerateCard;

    // 音声ファイルの入れ物
    AudioSource audioSource;
    public AudioClip kinngaku;
    public AudioClip rezi;
    public AudioClip hyousi1;
    public AudioClip hyousi2;

    int BuyID, CardID;
    int daikin = 300;
    bool changeFlag = false;

    void Start()
    {
        eventSystem = EventSystem.current;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        // ショップシーン上部の文字入れ替え
        if(changeFlag == false)
        {
            UPText.text = "具材の品質向上";
        }

        else if(changeFlag == true)
        {
            UPText.text = "どれと入れかえますか？";
        }
    }

    // buyパネル用
    // 具材購入のボタン
    public void kounyuButton()
    {
        // 押したボタンの取得
        ButtonObject = eventSystem.currentSelectedGameObject;
        GenerateCard = ButtonObject.GetComponent<GenerateCard>();

        // お金が足りていないかのチェック
        if(PanelChenge.Money > daikin)
        {
            buyItempanel.SetActive(false);
            buyCheckpanel.SetActive(true);
            PanelChenge.buyFlag = true;
            CardID = GenerateCard.GenerateID;
            audioSource.PlayOneShot(hyousi1);
        }

        // 足りてないとき
        else
        {
            buyItempanel.SetActive(false);
            MoneyPanel.SetActive(true);
            audioSource.PlayOneShot(hyousi2);
        }
        
    }

    // MoneyPanel(お金が無い事を教える)
    public void MoneyButton()
    {
        MoneyPanel.SetActive(false);
        if(buypanel.activeSelf == true)
        {
            buyItempanel.SetActive(true);
        }
        audioSource.PlayOneShot(hyousi1);
    }

    // buyCheckパネル用

    // キャンセル
    public void CancelButton()
    {
        buyCheckpanel.SetActive(false);
        buyItempanel.SetActive(true);
        PanelChenge.buyFlag = false;
        audioSource.PlayOneShot(hyousi2);
    }

    // 購入確定
    public void EnterButton()
    {
        changeFlag = true;
        Upgradepanel.SetActive(true);
        buyItempanel.SetActive(false);
        buyCheckpanel.SetActive(false);
        audioSource.PlayOneShot(kinngaku);
        
        Destroy(ButtonObject);
    }

    //　upgradeパネル用
    public void UpgradeButton()
    {
        ButtonObject = eventSystem.currentSelectedGameObject;
        GenerateCard = ButtonObject.GetComponent<GenerateCard>();
        // チェンジフラグがオフなら普通にアップグレード
        if(changeFlag == false)
        {
            if(GenerateCard.GenerateID % 3 == 1)
            {
                daikin = 300;
            }
            else
            {
                daikin = 600;
            }
            if(PanelChenge.Money > daikin)
            {
                Debug.Log(ButtonObject);
            
                // アップグレード処理
                // レベルチェック
                if(GenerateCard.GenerateID % 3 > 0)
                {
                    PanelChenge.Money -= daikin;
                    // idに1足してレベルを上げる
                    GenerateCard.GenerateID += 1;
                    // 配列に対しての処理
                    PanelChenge.myListPC[int.Parse(ButtonObject.name)] = GenerateCard.GenerateID;
                    // 生成及び名称変更、ボタンの設定と後処理
                    GameObject obj = Instantiate(ButtonObject,ButtonObject.transform.position, Quaternion.identity, Upgradepanel.transform);
                    obj.name = ButtonObject.name;
                    nakami = obj.GetComponent<Button>();
                    nakami.onClick.AddListener(UpgradeButton);
                    Destroy(ButtonObject);
                    audioSource.PlayOneShot(rezi);
                }
            }

            else
            {
                MoneyPanel.SetActive(true);
                audioSource.PlayOneShot(hyousi2);
            }
        }

        // チェンジフラグがオンならば交換にする
        else if(changeFlag == true)
        {
            if(GenerateCard.GenerateID % 3 == 1)
            {
                daikin = 200;
            }
            else if(GenerateCard.GenerateID % 3 == 2)
            {
                daikin = 300;
            }
            else
            {
                daikin = 400;
            }


            if(PanelChenge.Money > daikin)
            {
                changeFlag = false;
                PanelChenge.buyFlag = false;
                

                PanelChenge.Money -= daikin;
                
                //　Id調整
                if(GenerateCard.GenerateID % 3 == 2)
                {
                    CardID += 1;
                }

                else if(GenerateCard.GenerateID % 3 == 0)
                {
                    CardID += 2;
                }

                // 交換処理
                PanelChenge.myListPC[int.Parse(ButtonObject.name)] = CardID;

                // 同じ位置に生成
                GenerateCard.GenerateID = CardID;
                GameObject obj = Instantiate(ButtonObject,ButtonObject.transform.position, Quaternion.identity, Upgradepanel.transform);
                obj.name = ButtonObject.name;
                nakami = obj.GetComponent<Button>();
                nakami.onClick.AddListener(UpgradeButton);
                Destroy(ButtonObject);
                //　前の奴を削除
                Debug.Log(string.Join(", ",PanelChenge.myListPC));
                audioSource.PlayOneShot(hyousi1);
                buypanel.SetActive(false);
            }
            else
            {
                MoneyPanel.SetActive(true);
                audioSource.PlayOneShot(hyousi2);
            }
        }
    }
}
