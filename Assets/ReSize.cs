using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSize : MonoBehaviour
{
    public Vector2 screenSize;
    public Vector2 refSize;
    public float ratio;
    public float refRatio;
    public float percentage;

    public GameObject bush;
    public Vector2 bushScale;


    private void Awake()
    {
        bushScale = bush.transform.localScale;
        Resize();

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        Resize();
    }

    void Resize()
    {
        screenSize.x = Camera.main.pixelWidth;
        screenSize.y = Camera.main.pixelHeight;

        ratio = Mathf.Lerp(screenSize.x / screenSize.y, screenSize.y / screenSize.x, percentage);
        refRatio = Mathf.Lerp(refSize.x / refSize.y, refSize.y / refSize.x, percentage);
        

        if (ratio < refRatio)
        {
            transform.localScale = Vector3.one * ratio / refRatio;
            Vector2 strech = bushScale;


            strech.y *= refRatio / ratio;
            bush.transform.localScale = strech;




        }
        else
        {
            Vector2 strech = bushScale;
            strech.x *= ratio / refRatio;
            bush.transform.localScale = strech;
          
        }

    }
}
