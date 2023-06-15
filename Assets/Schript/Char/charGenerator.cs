using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charGenerator : MonoBehaviour
{
    public int nameCount;

    void Start()
    {
        GameObject[] Char = GameObject.FindGameObjectsWithTag("Char");
        foreach (GameObject obj in Char)
        {
            obj.SendMessage("Charset");
        }

        nameCount = 16;
    }

    void Update()
    {
        
    }
}
