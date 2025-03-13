using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroductionScript : MonoBehaviour
{
    public List<GameObject> speechBubble = new List<GameObject>();
    int speechIndex = 0;
    
    void Start()
    {
        StartCoroutine(introductionTimer());
    }

    IEnumerator introductionTimer() 
    {
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0;
        
    }

    private void OnMouseDown()
    {
        speechBubble[speechIndex].SetActive(false);
        speechIndex++;
        speechBubble[speechIndex].SetActive(true);
    }
}
