using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public int player1Score;
    public int player2Score;
    public float timer;
    public bool isOver;
    public bool GoldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;

    [Header("Prefab")]
    public GameObject[] BallPrefab;
    public GameObject[] powerUp;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("InGame UI")]
    public TextMeshProUGUI timertxt;
    public TextMeshProUGUI Player1ScoreTxt;
    public TextMeshProUGUI Player2ScoreTxt;
    public GameObject GoldenGoalUI;


    [Header("Game Over UI")]
    public GameObject Player1WinUI;
    public GameObject Player2WinUI;
    public GameObject YouWinUI;
    public GameObject YouLoseUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        Player1WinUI.SetActive(false);
        Player2WinUI.SetActive(false);
        YouWinUI.SetActive(false);
        YouLoseUI.SetActive(false);

        GoldenGoalUI.SetActive(false);

        timer = GameData.instance.GameTimer;
        isOver = false;
        GoldenGoal = false;
        FirstSpawn(GameData.instance.ball);
    }

    // Update is called once per frame
    void Update()
    {
        Player1ScoreTxt.text = player1Score.ToString();
        Player2ScoreTxt.text = player2Score.ToString();

        if(timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt( timer / 60);
            float seconds = Mathf.FloorToInt( timer % 60);
            timertxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (seconds % 10 == 0 && !isSpawnPowerUp) 
            {
                StartCoroutine("SpawnPowerUp");
            }
        }

        if (timer < 0f && !isOver) 
        {
            if (player1Score == player2Score)
            {
                if(!GoldenGoal)
                {
                    GoldenGoal = true;

                    Ball[] ball = FindObjectsOfType<Ball>();
                    for (int i = 0; i < ball.Length; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }

                    GoldenGoalUI.SetActive(true);

                    SpawnBall();
                }
            }

            else
            {
                GameOver();
            }

         
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.25f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        SoundManager.instance.UIClickSfx();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void SpawnBall()
    {
        Debug.Log("Muncul Bola");
        StartCoroutine(DelaySpawn(GameData.instance.ball));

    }

    public void FirstSpawn(int index)
    {
        Debug.Log("Muncul Bola");

        ballSpawned = Instantiate(BallPrefab[index], Vector3.zero, Quaternion.identity);
    }

    public void GameOver()
    {
        SoundManager.instance.GameOverSfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;

        GameOverPanel.SetActive(true);
        
        if(!GameData.instance.isSinglePlayer)
        {
            if (player1Score > player2Score)
            {
                Player1WinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                Player2WinUI.SetActive(true);
            }
        }

        else
        {
            if (player1Score > player2Score)
            {
                YouWinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                YouLoseUI.SetActive(true);
            }
        }
    }

    private IEnumerator DelaySpawn(int index)
    {
        yield return new WaitForSeconds(3);
        if (ballSpawned == null) 
        {
            ballSpawned = Instantiate(BallPrefab[index], Vector3.zero, Quaternion.identity);
        }
      
    }

  
}

