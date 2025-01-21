using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioMixer musicMixer;
   
    private void Awake()
    {
        instance = this;
        
    }

    public AudioSource leafSound, spiderSound, winSound, loseSound, gameSound;

    public void PlayLeafMatch()
    {
        if (spiderSound.isPlaying == false)
        {
            leafSound.Stop();

            leafSound.pitch = Random.Range(.8f, 1.2f);

            leafSound.Play();
        }

    }

    public void StopLeafMatch()
    {
        leafSound.Stop();
    }

    public void PlaySpiderMatch()
    {
        leafSound.Stop();

        spiderSound.Stop();

        spiderSound.pitch = Random.Range(.8f, 1.2f);

        spiderSound.Play();
    }

    public void PlayWinSound()
    {
        winSound.Play();
    }

    public void PlayLoseSound()
    {    
        loseSound.Play();
    }

    public void StopMusic() 
    {

        musicMixer.SetFloat("Music Volume",-80);
    
    }

    public void PlayMusic()
    {

        musicMixer.SetFloat("Music Volume", -20);

    }
}
