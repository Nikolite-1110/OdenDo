using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCS : MonoBehaviour
{
    public static List<int> myList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //myList = PanelChenge.kaesikaesi();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Join(", ",myList));
    }

    public static List<int> hairetukaesu()
    {
        return myList;
    }

    public void testtest()
    {
        myList.Add(1);
        myList.Add(4);
        myList.Add(7);
        myList.Add(10);
        myList.Add(13);
        myList.Add(16);
    } 
}
