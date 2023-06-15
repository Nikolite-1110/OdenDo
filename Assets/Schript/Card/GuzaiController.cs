using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuzaiController : MonoBehaviour
{
    public int GuzaiNum;
    public int GuzaiID;

    Sprite GuzaiSprite;
    ObjectMoving GuzaiMoving;

    
    // Start is called before the first frame update
    void Start()
    {
        SetGuzaiImage();
        GuzaiMoving = gameObject.GetComponent<ObjectMoving>();
        GuzaiMoving.fadeGenerate(new Vector3(0, -1, 0), 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuzaiDestroy(){
        GuzaiMoving.fadeErase(new Vector3(0, 1, 0), 0.3f);
    }

    public void SetGuzaiImage(){
        
        
        switch(GuzaiID){
            case 1:
            case 2:
            case 3:
                GuzaiSprite = Resources.Load<Sprite>("Daikon");
                break;

            case 4:
            case 5:
            case 6:
                GuzaiSprite = Resources.Load<Sprite>("Tamago");
                break;

            case 7:
            case 8:
            case 9:
                GuzaiSprite = Resources.Load<Sprite>("Chikuwa");
                break;

            case 10:
            case 11:
            case 12:
                GuzaiSprite = Resources.Load<Sprite>("Konbu");
                break;

            case 13:
            case 14:
            case 15:
                GuzaiSprite = Resources.Load<Sprite>("Gyusuzi");
                break;

            case 16:
            case 17:
            case 18:
                GuzaiSprite = Resources.Load<Sprite>("Hanpen");
                break;

            case 19:
            case 20:
            case 21:
                GuzaiSprite = Resources.Load<Sprite>("Chikuwabu");
                break;
            
            case 22:
            case 23:
            case 24:
                GuzaiSprite = Resources.Load<Sprite>("Mochikin");
                break;

            case 25:
            case 26:
            case 27:
                GuzaiSprite = Resources.Load<Sprite>("tsukune");
                break;

            case 28:
            case 29:
            case 30:
                GuzaiSprite = Resources.Load<Sprite>("Satumaage");
                break;

            case 31:
            case 32:
            case 33:
                GuzaiSprite = Resources.Load<Sprite>("Konnyaku");
                break;
        }

        gameObject.GetComponent<Image>().sprite = GuzaiSprite; 
    }
}
