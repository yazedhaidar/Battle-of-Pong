using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip uiButton;
    public AudioClip ballBounce;
    public AudioClip goal;
    public AudioClip gameOver;

    private AudioSource audio;
  

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameOver);
        }
        else
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audio.volume = GameData.instance.volume;
    }

  
    public void UIClickSfx()
    {
        audio.PlayOneShot(uiButton);
    }

    public void BallBounceSfx()
    {
        audio.PlayOneShot(ballBounce);
    }

    public void GoalSfx()
    {
        audio.PlayOneShot(goal);
    }

    public void GameOverSfx()
    {
        audio.PlayOneShot(gameOver);
    }

    public void turnOffVol()
    {
        GameData.instance.volume = 0;
        audio.volume = GameData.instance.volume;
    }

    public void turnOnVol()
    {
        GameData.instance.volume = 1f;
        audio.volume = GameData.instance.volume;
    }
}
