using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainPanel;
    public GameObject HTPPanel;
    public GameObject BallPanel;
    public GameObject TimerPanel;
    public GameObject musicOn;
    public GameObject musicOff;

    private void Awake()
    {
        GameData.instance.volume = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        MainPanel.SetActive(true);
        BallPanel.SetActive(false);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SinglePlayerButton()
    {
        GameData.instance.isSinglePlayer = true;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void MultiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void BackButton()
    {
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void SetTimerButton(float Timer)
    {
        GameData.instance.GameTimer = Timer;
        BallPanel.SetActive(true);
        TimerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void SetBallButton(int index)
    {
        GameData.instance.ball = index;
        HTPPanel.SetActive(true);
        BallPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Gameplay");
    
    }

    public void offVolume()
    {
        musicOff.SetActive(false);
      
        SoundManager.instance.turnOffVol();
        musicOn.SetActive(true);

    }

    public void onVolume()
    {
        musicOn.SetActive(false);
        SoundManager.instance.turnOnVol();
        musicOff.SetActive(true);
    }
    public void ExitGame()
    {

        Application.Quit();
    }
}
