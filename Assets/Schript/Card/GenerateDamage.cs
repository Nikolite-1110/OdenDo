using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateDamage : MonoBehaviour
{
    int DamegeText;
    GameObject DCard;
    GenerateCard generateDCard;

    // Start is called before the first frame update
    void Start()
    {
        DCard = transform.parent.gameObject;
        generateDCard = DCard.GetComponent<GenerateCard>(); 

        ChengeDamegeText();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChengeDamegeText(){
        switch (generateDCard.GenerateID){
                case 10:
                case 13:
                case 16:
                    DamegeText += 1;
                    break;
                    
                case 11:
                case 14:
                case 17:
                case 25:

                case 28:
                case 31:
                    DamegeText += 2;
                    break;

                case 12:
                case 19:

                case 29:
                case 32:
                    DamegeText += 3;
                    break;

                case 1:
                case 15:
                case 20:
                case 26:
                    DamegeText += 4;
                    break;

                case 4:
                case 7:
                case 18:
                case 21:
                    DamegeText += 5;
                    break;

                case 2:
                case 27:
                    DamegeText += 6;
                    break;

                case 5:
                case 8:
                    DamegeText += 7;
                    break;

                case 3:
                    DamegeText += 8;
                    break;

                case 6:
                case 9:
                    DamegeText += 9;
                    break;

                case 30:
                case 33:
                    DamegeText += 12;
                    break;

            }

        gameObject.GetComponent<TextMesh>().text = DamegeText.ToString();
    }
}
