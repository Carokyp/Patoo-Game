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

    private void Awake()
    {
        
        

        
    }

    void Start()
    {
        transform.localScale = new Vector3(screenSize.x/refSize.x, screenSize.y/refSize.y,1);
    }

    
    void Update()
    {
        screenSize.x = Camera.main.pixelWidth;
        screenSize.y = Camera.main.pixelHeight;

        ratio = Mathf.Lerp(screenSize.x / screenSize.y, screenSize.y / screenSize.x, percentage);
        refRatio = Mathf.Lerp(refSize.x / refSize.y, refSize.y / refSize.x, percentage);
        transform.localScale = Vector3.one * ratio / refRatio;
    }
}
