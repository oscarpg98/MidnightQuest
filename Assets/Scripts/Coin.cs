using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameManager gameManager;
    [SerializeField] private int scoreAddition;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerHitbox")) {
            gameManager.IncreasePlayerScore(scoreAddition);
            Destroy(gameObject);
        }
    }
}
