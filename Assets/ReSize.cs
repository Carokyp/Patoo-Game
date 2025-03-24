using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSize : MonoBehaviour
{
    public Vector2 screeSize;
    public Vector2 refSize;

    private void Awake()
    {
        screeSize.x = Camera.main.pixelWidth;
        screeSize.y = Camera.main.pixelHeight;
        // screeSize = Camera.main.ScreenToWorldPoint(screeSize);

        
    }

    void Start()
    {
        transform.localScale = new Vector3(screeSize.x/refSize.x, screeSize.y/refSize.y,1);
    }

    
    void Update()
    {
        
    }
}
