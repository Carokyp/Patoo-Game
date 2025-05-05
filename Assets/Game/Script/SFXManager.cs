using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioMixer musicMixer;
    public bool musicOn;
    static bool isPlayingMusic = true;
    public GameObject musicOnButton, musicOffButton;
 


    private void Awake()
    {
        instance = this;
               
    }

    private void Start()
    {
        SetMusic();
    }

    public AudioSource leafSound, spiderSound, winSound, loseSound, gameSound, superLeaf, starSound;

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

    public void PlaySuperLeafMatch()
    {
        leafSound.Stop();

        spiderSound.Stop();

        superLeaf.Play();

    }

    public void PlayStarSound()
    {
        starSound.Play();
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
        isPlayingMusic = false;
    
    }

    public void PlayMusic()
    {

        musicMixer.SetFloat("Music Volume", -20);
        isPlayingMusic = true;
    }

    public void SetMusic() 
    {
        musicOn = isPlayingMusic;
        if (isPlayingMusic == true)
        {
            musicOnButton.SetActive(true);
            musicOffButton.SetActive(false);
            PlayMusic();
        }
        else
        {
            musicOnButton.SetActive(false);
            musicOffButton.SetActive(true);
            StopMusic();
        }

    }
}
