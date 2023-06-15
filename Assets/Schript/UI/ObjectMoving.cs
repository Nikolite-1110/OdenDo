using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    public bool doFadeErase = false;
    public bool doFadeGenerate = false;
    public Vector3 dir;
    public float time;
    public float deltaAlpha;

    RectTransform ObjectPostion;
    Vector3 MovingPosition;

    // Start is called before the first frame update
    void Start()
    {
        //ObjectPostion = gameObject.GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(doFadeErase){
            MovingPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<CanvasGroup>().alpha -= deltaAlpha;

            MovingPosition += dir;
            gameObject.GetComponent<RectTransform>().anchoredPosition = MovingPosition;
            if(gameObject.GetComponent<CanvasGroup>().alpha <= 0){
                //Debug.Log(deltaAlpha.ToString());
                Destroy(gameObject);
            }
            
        }
        
        if(doFadeGenerate){
            MovingPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<CanvasGroup>().alpha += deltaAlpha;

            MovingPosition += dir;
            gameObject.GetComponent<RectTransform>().anchoredPosition = MovingPosition;
            if(gameObject.GetComponent<CanvasGroup>().alpha >= 1){
                //Debug.Log(deltaAlpha.ToString());
                doFadeGenerate = false;
            }
        }
    }

    public void fadeErase(Vector3 sendDir, float sendTime){
        dir = sendDir;
        time = sendTime;
        deltaAlpha = 1 / ((1f / Time.deltaTime) * time);
        if(gameObject.tag == "Char" || gameObject.tag == "CharTest"){
            gameObject.GetComponent<berController>().canAngryBack = false;
            gameObject.GetComponent<berController>().canBack = false;

            var DestroyRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            var DestroyBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            var DestroyCharPosition = gameObject.GetComponent<charPostion>();
            Destroy(DestroyCharPosition);
            Destroy(DestroyBoxCollider2D);
            Destroy(DestroyRigidbody2D);
        } else if (gameObject.tag == "card"){

        }
        doFadeErase = true;
        doFadeGenerate = false;
    }

    public void fadeGenerate(Vector3 sendDir, float sendTime){
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        dir = sendDir;
        time = sendTime;
        deltaAlpha = 1 / ((1f / Time.deltaTime) * time);
       
        doFadeGenerate = true;
    }
}
