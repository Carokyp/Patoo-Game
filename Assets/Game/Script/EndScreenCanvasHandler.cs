using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenCanvasHandler : MonoBehaviour
{
    public float screenW, screenH, ratio;
    public GameObject[] canvas;
   
    void Start()
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

                break;
            case < 2:
                canvas[0].SetActive(true);
                
                break;
            case <= 2.3f:
                canvas[1].SetActive(true);
                
                break;
            case >= 2.3f:
                canvas[3].SetActive(true);
                
                break;



            default:
                canvas[0].SetActive(true);
                
                break;
        }
    }

    
 
}
