using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string loadLevel;
    public void StarGame()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("ShowDialogue") == 0)
        {
            SceneManager.LoadScene(loadLevel);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
      
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestData() 
    {
        Debug.Log(PlayerPrefs.GetInt("Level"));
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("Level"));
    
    }
}
