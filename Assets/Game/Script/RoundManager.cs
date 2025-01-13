using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public float roundTime = 60f;
    private UIManager uiMan;

    private LevelManager levelManager;

    public GameObject replayButton;
    public GameObject nextButton;
    public GameObject pauseButton;
    public GameObject soundButton;
    public GameObject soundOffButton;
    public GameObject shufflePanel;

    public bool endingRound = false;
    private Board board;

    public int currentScore;
    public float displayScore;
    public float scoreSpeed;

    public int scoreTarget1, scoreTarget2, scoreTarget3;

    public float min;
    public float sec;

    private bool hasAddBeenShowed = false;


   
    
    void Awake()
    {
        Time.timeScale = 1;
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
        levelManager = FindObjectOfType<LevelManager>();
           
    }

    void Update()
    {
        if (currentScore < 0)
        {
            currentScore = 0;
        }

    
        if (roundTime > 0)
        {

            roundTime -= Time.deltaTime;

            min = Mathf.FloorToInt(roundTime / 60);
            sec = roundTime % 60;


            if (roundTime <= 0)
            {
                roundTime = 0;

                endingRound = true;
            }
        }

        if (endingRound && board.currentState ==  Board.BoardState.move)
        {
            WinCheck();
            shufflePanel.SetActive(false);
            endingRound = false;
        }

        uiMan.timeText.text = string.Format("{0:0} : {1:0} s", min, sec);

        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime);
        uiMan.scoreText.text = displayScore.ToString("0");

    }

    public void WinCheck() 
    {

        uiMan.roundOverScreen.SetActive(true);

        pauseButton.SetActive(false);
        soundButton.SetActive(false);
        soundOffButton.SetActive(false);

        uiMan.winScore.text = currentScore.ToString();

        

        if (currentScore >= scoreTarget3)
        {
            uiMan.winStar3.SetActive(true);
            uiMan.winDog3.SetActive(true);

            nextButton.SetActive(true);
            SFXManager.instance.PlayWinSound();
            

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star3", 1);

            if (PlayerPrefs.GetInt("Level") < levelManager.nextLevelToLoad -2)
            {
                PlayerPrefs.SetInt("Level", levelManager.nextLevelToLoad -2);
            }
            
            

        }
        else if (currentScore >= scoreTarget2)
        {
            uiMan.winStar2.SetActive(true);
            uiMan.winDog2.SetActive(true);

            nextButton.SetActive(true);

            SFXManager.instance.PlayWinSound();
            

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);

            if (PlayerPrefs.GetInt("Level") < levelManager.nextLevelToLoad -2)
            {
                PlayerPrefs.SetInt("Level", levelManager.nextLevelToLoad -2);
            }

        }
        else if (currentScore >= scoreTarget1)
        {
            uiMan.winStar1.SetActive(true);
            uiMan.winDog1.SetActive(true);


            nextButton.SetActive(true);

            SFXManager.instance.PlayWinSound();
            

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);


            if (PlayerPrefs.GetInt("Level") < levelManager.nextLevelToLoad -2)
            {
                Debug.Log(levelManager.nextLevelToLoad);
                PlayerPrefs.SetInt("Level", levelManager.nextLevelToLoad -2);
                Debug.Log(PlayerPrefs.GetInt("Level"));
            }

        }
        else
        {
            uiMan.winStar0.SetActive(true);
            uiMan.winDog0.SetActive(true);
            pauseButton.SetActive(false);
            soundButton.SetActive(false);
            soundOffButton.SetActive(false);

            replayButton.SetActive(true);

            
            SFXManager.instance.PlayLoseSound();

            Time.timeScale = 0;

        }

        if (hasAddBeenShowed == false)
        {
            ShowAdds.adShowed++;
            Debug.Log(ShowAdds.adShowed);
            /* StartCoroutine(AddTimer());*/
            hasAddBeenShowed = true;
        }
  
    }
    IEnumerator AddTimer() 
    {
        yield return new WaitForSeconds(2f);
        /*ShowAdds.adShowed++;
        Debug.Log(ShowAdds.adShowed);*/
    
    }

}
