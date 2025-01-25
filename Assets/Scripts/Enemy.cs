using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected int damageAttack;
    [SerializeField] AudioClip hurtSound;
    protected Vector3 currentDestination;
    protected int currentIndex;

    public AudioClip HurtSound { get => hurtSound; }

    protected abstract IEnumerator Attack();

    protected virtual void Start() {
        if (waypoints.Length > 0) {
            currentDestination = waypoints[currentIndex].position;
            StartCoroutine(Patrol());
        }
    }

    protected virtual void DefineNewDestination() {
        currentIndex++;
        currentIndex %= waypoints.Length;
        currentDestination = waypoints[currentIndex].position;
        FlipRotation();
    }

    protected virtual void FlipRotation() {
        if (currentDestination.x > transform.position.x) {
            transform.localScale = Vector3.one;
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected IEnumerator Patrol() {
        while (true) {
            while (transform.position != currentDestination) {
                transform.position = Vector3.MoveTowards(transform.position, currentDestination, patrolSpeed * Time.deltaTime);
                yield return null;
            }
            DefineNewDestination();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerDetection")) {
            Debug.Log("Player detectado");
        }
        else if (other.CompareTag("PlayerHitbox")) {
            LivesSystem livesSystemPlayer = other.GetComponent<LivesSystem>();
            AudioClip hurtSound = other.GetComponent<Player>().HurtSound;
            AudioManager.Instance.PlaySoundEffect(hurtSound);
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }
}
