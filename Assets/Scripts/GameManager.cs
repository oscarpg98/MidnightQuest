using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxLevel;
    [SerializeField] AudioClip gameOverSound;

    private int playerHealth;
    private int playerScore;
    private int level;

    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public int Level { get => level; }
    public int MaxLevel { get => maxLevel; }

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private bool isPaused = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        playerHealth = 100;
        playerScore = 0;
        string sceneName;
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "MainMenu") {
            char lastChar = sceneName[sceneName.Length - 1];

            if (char.IsDigit(lastChar)) {
                level = int.Parse(lastChar.ToString());
            }
        }
    }

    private void Update() {
        if (playerHealth <= 0) {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void UpdatePlayerLife(int newHealth) {
        playerHealth = newHealth;
        if (playerHealth >= 100) playerHealth = maxHealth;
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        if (healthText != null) {
            healthText.text = "Health: " + playerHealth.ToString();
        }
    }

    public void IncreasePlayerScore(int addedScore) {
        playerScore += addedScore;
        UpdateUIScore();
    }

    private void UpdateUIScore() {
        if (healthText != null) {
            scoreText.text = "Score: " + playerScore.ToString();
        }
    }

    private void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) {
            gameOverText.text = "pause";
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameOver() {
        if (gameOverScreen != null) {
            gameOverText.text = "game over";
            gameOverScreen.SetActive(true);
            AudioManager.Instance.PlaySoundEffect(gameOverSound);
        }

        Time.timeScale = 0; // Pausa el juego
    }

    public void Win() {
        if (gameOverScreen != null) {
            gameOverText.text = "you won!";
            gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0; // Pausa el juego
    }

    public void RestartGame() {
        Time.timeScale = 1; // Reanuda el juego
        playerHealth = 100;
        playerScore = 0;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("Level_" + level);
    }
    public void MainMenu() {
        RestartGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void AdvanceLevel() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            level++;
            if (level > maxLevel) {
                level = 1;
            }            
        }
    }
}
