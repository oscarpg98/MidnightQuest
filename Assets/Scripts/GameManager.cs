using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public TextMeshProUGUI healthText;

    private int playerHealth;


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
    }

    public void UpdatePlayerLife(int newHealth) {
        playerHealth = newHealth;
        Debug.Log(playerHealth);
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        if (healthText != null) {
            healthText.text = "Health: " + playerHealth.ToString();
        }
    }
}
