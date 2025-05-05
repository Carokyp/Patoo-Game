using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public float screenW, screenH, ratio;
    public GameObject[] canvas;
    public RoundManager roundManager;
    public MatchFinder matchFinder;
    public Board board;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        matchFinder = FindObjectOfType<MatchFinder>();
        board = FindObjectOfType<Board>();
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
                matchFinder.uiMan = canvas[2].GetComponent<UIManager>();
                board.uiMan = canvas[2].GetComponent<UIManager>();
                roundManager.levelManager = canvas[2].GetComponent<LevelManager>();
                break;
            case < 2:
                canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[0].GetComponent<UIManager>();
                board.uiMan = canvas[0].GetComponent<UIManager>();
                roundManager.levelManager = canvas[0].GetComponent<LevelManager>();
                break;
            case <= 2.3f:
                canvas[1].SetActive(true);
                roundManager.uiMan = canvas[1].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[1].GetComponent<UIManager>();
                board.uiMan = canvas[1].GetComponent<UIManager>();
                roundManager.levelManager = canvas[1].GetComponent<LevelManager>();
                break;
            case >= 2.3f:
                canvas[3].SetActive(true);
                roundManager.uiMan = canvas[3].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[3].GetComponent<UIManager>();
                board.uiMan = canvas[3].GetComponent<UIManager>();
                roundManager.levelManager = canvas[3].GetComponent<LevelManager>();
                break;
       


            default: canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[0].GetComponent<UIManager>();
                board.uiMan = canvas[0].GetComponent<UIManager>();
                roundManager.levelManager = canvas[0].GetComponent<LevelManager>();
                break;
        }
    }
    
}
