using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private int damageAttack;
    private Vector3 currentDestination;
    private int currentIndex;

    // Start is called before the first frame update
    void Start() {
        currentDestination = waypoints[currentIndex].position;
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update() {

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

        }
        else if (other.CompareTag("PlayerHitbox")) {
            LivesSystem livesSystemPlayer = GetComponent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }
}
