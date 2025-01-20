using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;
    public bool isGamePaused = false;
    private int score;
    public int lives;
    private float spawnRate = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }
    public void UpdateLives(int livesToAdd)
    {
        lives -= livesToAdd;
        if (lives <= 0)
        {
            livesText.text = "Lives : " + 0;
            GameOver();
        }
        else
        {
            livesText.text = "Lives : " + lives;
        }
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
    }

    void TogglePause()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }
        isGamePaused = !isGamePaused;
    }
}
