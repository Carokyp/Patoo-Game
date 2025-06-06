using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text scoreToReach;

    public TMP_Text winScore;
    public TMP_Text winText;
    public GameObject winStar0, winStar1, winStar2, winStar3;
    public GameObject star1, star2, star3;
    public GameObject winDog0, winDog1, winDog2, winDog3;
    public GameObject happyDog,sadDog;
    public GameObject lostPanel;

    public GameObject replayButton;
    public GameObject nextButton;
    public GameObject pauseButton;
    public GameObject soundButton;
    public GameObject helpButton;
    public GameObject soundOffButton;
    public GameObject shufflePanel;

    public SFXManager soundManager;

    public GameObject roundOverScreen;

    private void Awake()
    {
        soundManager = FindObjectOfType<SFXManager>();
    }

    void Start()
    {


        winStar1.SetActive(false);
        winStar1.SetActive(false);
        winStar2.SetActive(false);
        winStar3.SetActive(false);
        
        winDog0.SetActive(false);
        winDog1.SetActive(false);
        winDog2.SetActive(false);
        winDog3.SetActive(false);

        happyDog.SetActive(false);
        sadDog.SetActive(false);

        lostPanel.SetActive(false);

        soundManager.musicOnButton = soundButton;
        soundManager.musicOffButton = soundOffButton;
        soundManager.SetMusic();
    }

}
