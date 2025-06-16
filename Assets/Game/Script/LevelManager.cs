using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class LevelManager : MonoBehaviour
{
    public int nextLevelToLoad;

    public int levelToReplay;


    void Start()
    {
        
    }

    
    public void LoadLevel()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(

            "Levels Progress", new Dictionary<string, object> {
                {"Level", nextLevelToLoad} });

        Debug.LogWarning("Analytics" + analyticsResult);

        SceneManager.LoadScene(nextLevelToLoad);

    }

    public void Replay() 
    {

        SceneManager.LoadScene(levelToReplay);
        
    
    }
}
