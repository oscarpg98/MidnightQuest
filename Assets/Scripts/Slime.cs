using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private int damageAttack;
    private Vector3 currentDestination;
    private int currentIndex;


    void Start() {
        currentDestination = waypoints[currentIndex].position;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol() {
        while (true) {
            while (transform.position != currentDestination) {
                transform.position = Vector3.MoveTowards(transform.position, currentDestination, patrolSpeed * Time.deltaTime);
                yield return null;
            }
            DefineNewDestination();
        }
    }

    private void DefineNewDestination() {
        currentIndex++;
        currentIndex %= waypoints.Length;
        currentDestination = waypoints[currentIndex].position;
        FlipRotation();
    }

    private void FlipRotation() {
        if (currentDestination.x > transform.position.x) {
            transform.localScale = Vector3.one;
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerDetection")) {
            Debug.Log("Player detectado");
        }
        else if(other.CompareTag("PlayerHitbox")) {
            LivesSystem livesSystemPlayer = other.GetComponent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }
}
