using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public float screenW, screenH, ratio;
    public GameObject[] canvas;
    public RoundManager roundManager;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }


    private void Start()
    {     
        Screen.orientation = ScreenOrientation.LandscapeRight;
        screenW = Screen.width;
        screenH = Screen.height;
        ratio = screenW / screenH;

        foreach (GameObject item in canvas)
        {
            item.SetActive(false);
        }

        switch (ratio)
        {
            case < 1.5f:
                canvas[2].SetActive(true);
                roundManager.uiMan = canvas[2].GetComponent<UIManager>();
                break;
            case < 2:
                canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                break;
            case <= 2.3f:
                canvas[1].SetActive(true);
                roundManager.uiMan = canvas[1].GetComponent<UIManager>();
                break;
            case >= 2.3f:
                canvas[3].SetActive(true);
                roundManager.uiMan = canvas[3].GetComponent<UIManager>();
                break;
       


            default: canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                break;
        }
    }
    
}
