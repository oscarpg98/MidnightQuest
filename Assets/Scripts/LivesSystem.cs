using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private bool isPlayer;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private int coinScore;

    public void ReceiveDamage(int damageReceived) {
        health -= damageReceived;
        if (isPlayer) {
            GameManager.Instance.UpdatePlayerLife(health);
        }

        if (health <= 0) {
            GameManager.Instance.IncreasePlayerScore(coinScore);
            Destroy(gameObject);
        }
        if (health <= 0 && !isPlayer) {
            float powerUpProbability = Random.value;
            if (powerUpProbability <= 0.4f) {
                Instantiate(powerUps[0], transform.position, Quaternion.identity); // Medkit
            }
            else if (powerUpProbability <= 0.8f) {
                Instantiate(powerUps[1], transform.position, Quaternion.identity); // Double Jump
            }
        }
    }
}
