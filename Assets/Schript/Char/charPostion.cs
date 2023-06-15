using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charPostion : MonoBehaviour
{
    //横位置
    public int HPos;
    //縦位置
    public int BPos;

    Rigidbody2D rigid;

    public void charMove(int Hmove,int Bmove)
    {
        if (Hmove == HPos && Bmove < BPos)
        {
            BPos -= 1;
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -1);
    }

    void Update()
    {
        if(rigid.velocity.y > 0){
            rigid.velocity = new Vector2(0, 0);
        }

        if(rigid.velocity.y < -400){
            rigid.velocity = new Vector2(0, -400);
        }
    }
}
