using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateText : MonoBehaviour
{
    string CardText;
    GameObject TCard;
    GenerateCard generateTCard;

    // Start is called before the first frame update
    void Start()
    {
        TCard = transform.parent.gameObject;
        generateTCard = TCard.GetComponent<GenerateCard>(); 

        ChengeText();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChengeText(){
        switch(generateTCard.GenerateID){
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                CardText = "単体ダメージ";
                break;
            case 10:
                CardText = "周囲拡散\n(25%)";
                break;
            case 11:
                CardText = "周囲拡散\n(50%)";
                break;
            case 12:
                CardText = "周囲拡散\n(70%)";
                break;
            case 13:
                CardText = "縦列3人貫通";
                break;
            case 14:
                CardText = "縦列4人貫通";
                break;
            case 15:
                CardText = "縦列5人貫通";
                break;
            case 16:
                CardText = "横列１列拡散";
                break;
            case 17:
                CardText = "横列２列拡散";
                break;
            case 18:
                CardText = "横列２列拡散";
                break;
            case 19:
            case 20:
            case 21:
                CardText = "2色持ち";
                break;
            case 22:
                CardText = "次回満腹値\n1.5倍";
                break;
            case 23:
                CardText = "次回満腹値\n1.75倍";
                break;
            case 24:
                CardText = "次回満腹値\n2.0倍";
                break;
            case 25:
                CardText = "帰宅まで\n＋4秒";
                break;
            case 26:
                CardText = "帰宅まで\n＋8秒";
                break;
            case 27:
                CardText = "帰宅まで\n＋12秒";
                break;

            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
                CardText = "Lv3だと\n"+"満腹値高め";
                break;

            default:
                CardText = "　　";
                break;
        }

        gameObject.GetComponent<TextMesh>().text = CardText;
    }
}
