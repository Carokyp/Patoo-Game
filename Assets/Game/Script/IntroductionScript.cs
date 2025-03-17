using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroductionScript : MonoBehaviour
{
    public GameObject pauseButton, helpButton;
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
        speechBubble[speechIndex].SetActive(true);
    }

    IEnumerator StartGame()
    {
        Time.timeScale = 1;
        speechBubble[speechIndex].SetActive(false);
        yield return new WaitForSeconds(0.8f);
        
    }

    private void OnMouseDown()
    {
        if (speechIndex < speechBubble.Count -1)
        {
            speechBubble[speechIndex].SetActive(false);

            speechIndex++;
            speechBubble[speechIndex].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            speechBubble[speechIndex].SetActive(false);
            gameObject.SetActive(false);
            pauseButton.SetActive(true);
            helpButton.SetActive(true);
        }
        
    }
}
