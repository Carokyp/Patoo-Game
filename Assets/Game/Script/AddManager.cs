using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddManager : MonoBehaviour
{
    public InterstitialAdExample interstitial;
    public AdsInitializer initializer;

    public static AddManager instance;
    public  int gamesPlayed;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        initializer.InitializeAds();
        interstitial.LoadAd();
    }

}
