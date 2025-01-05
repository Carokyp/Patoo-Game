using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
   
    void Update()
    {
        
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
}
