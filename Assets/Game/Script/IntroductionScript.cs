using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroductionScript : MonoBehaviour
{
    public GameObject pauseButton, helpButton, nextButton;
    public List<GameObject> speechBubble = new List<GameObject>();
    int speechIndex = 0;
    public Vector2 screenSize;
    private bool playAnimation = false;

    private void Awake()
    {
        screenSize.x = Camera.main.pixelWidth;
        screenSize.y = Camera.main.pixelHeight;
       
        transform.localScale = screenSize;
    }

    void Start()
    {
        Debug.Log("Star timer");

        StartCoroutine(introductionTimer());
    }

    IEnumerator introductionTimer() 
    {
        Debug.Log("timer as started");
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 0;
        speechBubble[speechIndex].SetActive(true);
    }

    IEnumerator StartGame()
    {
        Time.timeScale = 1;
        speechBubble[speechIndex].SetActive(false);
        yield return new WaitForSeconds(0.9f);
        
    }

    private void OnMouseDown()
    {
        if (speechIndex < speechBubble.Count -1)
        {
            speechBubble[speechIndex].SetActive(false);

            speechIndex++;
            speechBubble[speechIndex].SetActive(true);
            if (speechIndex >= speechBubble.Count -1)
            {
                if (!playAnimation)
                {
                  Time.timeScale = 1;
                  StartCoroutine(AnimationTimer());
                  playAnimation = true;
                }

            }
        }
        else
        {
           
            
        }
        
    }

    IEnumerator AnimationTimer()
    {
        //speechBubble[speechIndex].SetActive(false);
        //speechBubble[speechBubble.Count -1].SetActive(true);
        Animator handAnimator = speechBubble[speechBubble.Count -1].GetComponent<Animator>();
        nextButton.SetActive(false);

        yield return new WaitForSeconds(2f);

        handAnimator.Play("Hand Vertical Animation");

        yield return new WaitForSeconds(2f);

        speechBubble[speechIndex].SetActive(false);
            
        gameObject.SetActive(false);
        pauseButton.SetActive(true);
        helpButton.SetActive(true);
        
    }
}
