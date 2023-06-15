using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Script : MonoBehaviour
{
    public GameObject SetumeiPanel;
    public GameObject Setumei1;
    public GameObject Setumei2;
    public GameObject Setumei3;
    public GameObject TextPanel;

    int PanelNumber = 999;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetumeiOpen()
    {
        SetumeiPanel.SetActive(true);
        TextPanel.SetActive(false);
        Setumei1.SetActive(true);
        Setumei2.SetActive(false);
        Setumei3.SetActive(false);
        int PanelNumber = 999;
    }

    public void closeButton()
    {
        SetumeiPanel.SetActive(false);
        TextPanel.SetActive(true);
        Setumei1.SetActive(false);
        Setumei2.SetActive(false);
        Setumei3.SetActive(false);
    }

    public void LeftButton()
    {
        PanelNumber++;
        PanelChange();
    }

    public void RightButton()
    {
        PanelNumber--;
        PanelChange();
    }
    public void PanelChange()
    {
        switch(PanelNumber % 3){
            case 0:
                Setumei1.SetActive(true);
                Setumei2.SetActive(false);
                Setumei3.SetActive(false);
                break;
            case 1:
                Setumei1.SetActive(false);
                Setumei2.SetActive(true);
                Setumei3.SetActive(false);
                break;
            case 2:
                Setumei1.SetActive(false);
                Setumei2.SetActive(false);
                Setumei3.SetActive(true);
                break;
        }
    }
}
