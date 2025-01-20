using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    private Rigidbody2D rb;
    [SerializeField] private float shootingImpulse;
    [SerializeField] private float timeAlive;
    [SerializeField] private int damageAttack;
    private float timer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shootingImpulse, ForceMode2D.Impulse);
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer >= timeAlive) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("PlayerHitbox")) {
            LivesSystem livesSystemPlayer = other.collider.GetComponentInParent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
            Destroy(gameObject);
        }
    }
}
