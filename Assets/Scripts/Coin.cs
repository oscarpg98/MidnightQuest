using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameManager gameManager;
    [SerializeField] private int scoreAddition;
    [SerializeField] private AudioClip coinSound;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerHitbox")) {
            gameManager.IncreasePlayerScore(scoreAddition);
            AudioManager.Instance.PlaySoundEffect(coinSound);
            Destroy(gameObject);
        }
    }
}
