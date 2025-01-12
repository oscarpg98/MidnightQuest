using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool isPlayer;
    
    public void ReceiveDamage(int damageReceived) {
        health -= damageReceived;
        if (isPlayer) {
            GameManager.Instance.UpdatePlayerLife(health);
        }

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
