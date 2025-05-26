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
                roundManager.star1 = canvas[2].GetComponent<UIManager>().star1;
                roundManager.star2 = canvas[2].GetComponent<UIManager>().star2;
                roundManager.star3 = canvas[2].GetComponent<UIManager>().star3;
                roundManager.replayButton = canvas[2].GetComponent<UIManager>().replayButton;
                roundManager.helpButton = canvas[2].GetComponent<UIManager>().helpButton;
                roundManager.nextButton = canvas[2].GetComponent<UIManager>().nextButton;
                roundManager.pauseButton = canvas[2].GetComponent<UIManager>().pauseButton;
                roundManager.soundButton = canvas[2].GetComponent<UIManager>().soundButton;
                roundManager.soundOffButton = canvas[2].GetComponent<UIManager>().soundOffButton;
                break;
            case < 2:
                canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[0].GetComponent<UIManager>();
                board.uiMan = canvas[0].GetComponent<UIManager>();
                roundManager.levelManager = canvas[0].GetComponent<LevelManager>();
                roundManager.star1 = canvas[0].GetComponent<UIManager>().star1;
                roundManager.star2 = canvas[0].GetComponent<UIManager>().star2;
                roundManager.star3 = canvas[0].GetComponent<UIManager>().star3;
                roundManager.replayButton = canvas[0].GetComponent<UIManager>().replayButton;
                roundManager.helpButton = canvas[0].GetComponent<UIManager>().helpButton;
                roundManager.nextButton = canvas[0].GetComponent<UIManager>().nextButton;
                roundManager.pauseButton = canvas[0].GetComponent<UIManager>().pauseButton;
                roundManager.soundButton = canvas[0].GetComponent<UIManager>().soundButton;
                roundManager.soundOffButton = canvas[0].GetComponent<UIManager>().soundOffButton;
                break;
            case <= 2.3f:
                canvas[1].SetActive(true);
                roundManager.uiMan = canvas[1].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[1].GetComponent<UIManager>();
                board.uiMan = canvas[1].GetComponent<UIManager>();
                roundManager.levelManager = canvas[1].GetComponent<LevelManager>();
                roundManager.star1 = canvas[1].GetComponent<UIManager>().star1;
                roundManager.star2 = canvas[1].GetComponent<UIManager>().star2;
                roundManager.star3 = canvas[1].GetComponent<UIManager>().star3;
                roundManager.replayButton = canvas[1].GetComponent<UIManager>().replayButton;
                roundManager.helpButton = canvas[1].GetComponent<UIManager>().helpButton;
                roundManager.nextButton = canvas[1].GetComponent<UIManager>().nextButton;
                roundManager.pauseButton = canvas[1].GetComponent<UIManager>().pauseButton;
                roundManager.soundButton = canvas[1].GetComponent<UIManager>().soundButton;
                roundManager.soundOffButton = canvas[1].GetComponent<UIManager>().soundOffButton;
                break;
            case >= 2.3f:
                canvas[3].SetActive(true);
                roundManager.uiMan = canvas[3].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[3].GetComponent<UIManager>();
                board.uiMan = canvas[3].GetComponent<UIManager>();
                roundManager.levelManager = canvas[3].GetComponent<LevelManager>();
                roundManager.star1 = canvas[3].GetComponent<UIManager>().star1;
                roundManager.star2 = canvas[3].GetComponent<UIManager>().star2;
                roundManager.star3 = canvas[3].GetComponent<UIManager>().star3;
                roundManager.replayButton = canvas[3].GetComponent<UIManager>().replayButton;
                roundManager.helpButton = canvas[3].GetComponent<UIManager>().helpButton;
                roundManager.nextButton = canvas[3].GetComponent<UIManager>().nextButton;
                roundManager.pauseButton = canvas[3].GetComponent<UIManager>().pauseButton;
                roundManager.soundButton = canvas[3].GetComponent<UIManager>().soundButton;
                roundManager.soundOffButton = canvas[3].GetComponent<UIManager>().soundOffButton;
                break;
       


            default: canvas[0].SetActive(true);
                roundManager.uiMan = canvas[0].GetComponent<UIManager>();
                matchFinder.uiMan = canvas[0].GetComponent<UIManager>();
                board.uiMan = canvas[0].GetComponent<UIManager>();
                roundManager.levelManager = canvas[0].GetComponent<LevelManager>();
                roundManager.star1 = canvas[0].GetComponent<UIManager>().star1;
                roundManager.star2 = canvas[0].GetComponent<UIManager>().star2;
                roundManager.star3 = canvas[0].GetComponent<UIManager>().star3;
                roundManager.replayButton = canvas[0].GetComponent<UIManager>().replayButton;
                roundManager.helpButton = canvas[0].GetComponent<UIManager>().helpButton;
                roundManager.nextButton = canvas[0].GetComponent<UIManager>().nextButton;
                roundManager.pauseButton = canvas[0].GetComponent<UIManager>().pauseButton;
                roundManager.soundButton = canvas[0].GetComponent<UIManager>().soundButton;
                roundManager.soundOffButton = canvas[0].GetComponent<UIManager>().soundOffButton;
                break;
        }
    }
    
}
