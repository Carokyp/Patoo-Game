using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public float roundTime = 60f;
    public UIManager uiMan;

    public LevelManager levelManager;

    public GameObject replayButton;
    public GameObject nextButton;
    public GameObject pauseButton;
    public GameObject soundButton;
    public GameObject helpButton;
    public GameObject soundOffButton;
    public GameObject shufflePanel;
    public GameObject star1, star2, star3;

    public bool endingRound = false;
    private Board board;

    public int currentScore;
    public float displayScore;
    public float scoreToReach1, scoreToReach2, scoreToReach3;
    public float scoreSpeed;

    public int scoreTarget1, scoreTarget2, scoreTarget3;
    


    public float min;
    public float sec;

    private bool hasAddBeenShowed = false;
   
    
    void Awake()
    {
       
        board = FindObjectOfType<Board>();
        levelManager = FindObjectOfType<LevelManager>();
           
    }

    private void Start()
    {
        Time.timeScale = 1;
        
    }

    void Update()
    {

        ScoreTargetCheck();

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

        if (roundTime > 0)
        {
            uiMan.timeText.text = string.Format("{0:0} : {1:0} s", min, sec);
        }

      

        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime);
        uiMan.scoreText.text = displayScore.ToString("0");

        

    }

    public void ScoreTargetCheck()
    {


        if (currentScore >= scoreTarget3)
        {
            SFXManager.instance.PlayStarSound();

            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            


            uiMan.scoreToReach.text = scoreToReach3.ToString("0");

        }
        else if (currentScore >= scoreTarget2)
        {
            SFXManager.instance.PlayStarSound();

            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            
            uiMan.scoreToReach.text = scoreToReach3.ToString("0");
        }
        else if (currentScore >= scoreTarget1)
        {
            SFXManager.instance.PlayStarSound();

            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
           

            uiMan.scoreToReach.text = scoreToReach2.ToString("0");

        }
        else if (currentScore < scoreTarget1)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            

            uiMan.scoreToReach.text = scoreToReach1.ToString("0");

        }


    }

    public void WinCheck() 
    {

        uiMan.roundOverScreen.SetActive(true);
        
        uiMan.pauseButton.SetActive(false);
        uiMan.soundButton.SetActive(false);
        uiMan.soundOffButton.SetActive(false);
        uiMan.helpButton.SetActive(false);

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
            helpButton.SetActive(false);

            replayButton.SetActive(true);

            uiMan.lostPanel.SetActive(true);
            uiMan.winText.gameObject.SetActive(false);
            nextButton.SetActive(false);

            
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
