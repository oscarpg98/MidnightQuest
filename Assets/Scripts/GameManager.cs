using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    [SerializeField] private int maxHealth;

    private int playerHealth;
    private int playerScore;

    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }

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
        }

        Time.timeScale = 0; // Pausa el juego
    }

    public void RestartGame() {
        Time.timeScale = 1; // Reanuda el juego
        playerHealth = 100;
        playerScore = 0;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("Level_1");
    }
    public void MainMenu() {
        RestartGame();
        SceneManager.LoadScene("MainMenu");
    }
}
