using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField]GameObject EndPanel;
    [SerializeField]GameObject CresitPanel;

    [SerializeField]GameObject StartButton;
    [SerializeField]GameObject tyutoButton;
    [SerializeField]GameObject heitenButton;
    [SerializeField]GameObject cresitButton;
    AudioSource audioSource;
    public AudioClip hyousigi;
    public AudioClip hyousigi2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void heiten()
    {
        EndPanel.SetActive(true);
        StartButton.SetActive(false);
        heitenButton.SetActive(false);
        tyutoButton.SetActive(false);
        cresitButton.SetActive(false);
        audioSource.PlayOneShot(hyousigi);
    }

    public void backEnd()
    {
        EndPanel.SetActive(false);
        StartButton.SetActive(true);
        heitenButton.SetActive(true);
        tyutoButton.SetActive(true);
        cresitButton.SetActive(true);
        audioSource.PlayOneShot(hyousigi2);
    }

    public void endButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }

    public void cresit()
    {
        CresitPanel.SetActive(true);
        StartButton.SetActive(false);
        heitenButton.SetActive(false);
        tyutoButton.SetActive(false);
        cresitButton.SetActive(false);
        audioSource.PlayOneShot(hyousigi);
    }

    public void endCresit()
    {
        CresitPanel.SetActive(false);
        StartButton.SetActive(true);
        heitenButton.SetActive(true);
        tyutoButton.SetActive(true);
        cresitButton.SetActive(true);
        audioSource.PlayOneShot(hyousigi2);
    }
}
