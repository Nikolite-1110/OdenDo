using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateCard : MonoBehaviour
{
    public int GenerateID = 1;//このオブジェクト生成した後に必ず設定すること

    Sprite CardSprite;

    // Start is called before the first frame update
    void Start()
    {
        ChangeCardImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeCardImage()
    {
        switch(GenerateID){
            case 1:
            case 10:
            case 22:
            case 31:
                CardSprite = Resources.Load<Sprite>("greencard1");
                break;
            
            case 2:
            case 11:
            case 23:
            case 32:
                CardSprite = Resources.Load<Sprite>("greencard2");
                break;

            case 3:
            case 12:
            case 24:
            case 33:
                CardSprite = Resources.Load<Sprite>("greencard3");
                break;

            case 4:
            case 13:
            case 25:
                CardSprite = Resources.Load<Sprite>("redcard1");
                break;
            
            case 5:
            case 14:
            case 26:
                CardSprite = Resources.Load<Sprite>("redcard2");
                break;

            case 6:
            case 15:
            case 27:
                CardSprite = Resources.Load<Sprite>("redcard3");
                break;

            case 7:
            case 16:
            case 28:
                CardSprite = Resources.Load<Sprite>("bluecard1");
                break;

            case 8:
            case 17:
            case 29:
                CardSprite = Resources.Load<Sprite>("bluecard2");
                break;

            case 9:
            case 18:
            case 30:
                CardSprite = Resources.Load<Sprite>("bluecard3");
                break;

            case 19:
                CardSprite = Resources.Load<Sprite>("B&Rcard1");
                break;

            case 20:
                CardSprite = Resources.Load<Sprite>("B&Rcard2");
                break;

            case 21:
                CardSprite = Resources.Load<Sprite>("B&Rcard3");
                break;
        }

        gameObject.GetComponent<Image>().sprite = CardSprite; 
    }
}
