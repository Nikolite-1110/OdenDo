using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateImage : MonoBehaviour
{
    Sprite ChangeSprite;

    GameObject ICard;
    GenerateCard generateICard;

    // Start is called before the first frame update
    void Start()
    {
        ICard = transform.parent.gameObject;
        generateICard = ICard.GetComponent<GenerateCard>(); 

        ChengeImage();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChengeImage(){
        switch(generateICard.GenerateID){
            case 1:
            case 2:
            case 3:
                ChangeSprite = Resources.Load<Sprite>("Daikon");
                break;

            case 4:
            case 5:
            case 6:
                ChangeSprite = Resources.Load<Sprite>("Tamago");
                break;

            case 7:
            case 8:
            case 9:
                ChangeSprite = Resources.Load<Sprite>("Chikuwa");
                break;

            case 10:
            case 11:
            case 12:
                ChangeSprite = Resources.Load<Sprite>("Konbu");
                break;

            case 13:
            case 14:
            case 15:
                ChangeSprite = Resources.Load<Sprite>("Gyusuzi");
                break;

            case 16:
            case 17:
            case 18:
                ChangeSprite = Resources.Load<Sprite>("Hanpen");
                break;

            case 19:
            case 20:
            case 21:
                ChangeSprite = Resources.Load<Sprite>("Chikuwabu");
                break;
            
            case 22:
            case 23:
            case 24:
                ChangeSprite = Resources.Load<Sprite>("Mochikin");
                break;

            case 25:
            case 26:
            case 27:
                ChangeSprite = Resources.Load<Sprite>("tsukune");
                break;

            case 28:
            case 29:
            case 30:
                ChangeSprite = Resources.Load<Sprite>("Satumaage");
                break;

            case 31:
            case 32:
            case 33:
                ChangeSprite = Resources.Load<Sprite>("Konnyaku");
                break;
        }

        gameObject.GetComponent<Image>().sprite = ChangeSprite; 
    }
}
