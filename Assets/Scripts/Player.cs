using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private float inputH;
    private Animator animator;

    [Header("Movement System")]
    [SerializeField] private Transform feet;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask isJumpable;

    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    [Header("Combat System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damageAttack;
    [SerializeField] private LayerMask isDamageable;
    [SerializeField] private LayerMask isInteractable;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        Movement();
        Jump();
        ReleaseAttack();
        Interact();
    }

    private void Movement() {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * movementSpeed, rb.velocity.y);

        if (inputH != 0) {
            animator.SetBool("running", true);
            transform.eulerAngles = (inputH > 0) ? Vector3.zero : new Vector3(0, 180, 0);
        }
        else {
            animator.SetBool("running", false);
        }
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Grounded()) {
                PerformJump();
                hasDoubleJumped = false;
            }
            else if (canDoubleJump && !hasDoubleJumped) {
                PerformJump();
                hasDoubleJumped = true;
            }
        }
    }

    private void PerformJump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    private bool Grounded() {
        return Physics2D.Raycast(feet.position, Vector3.down, rayDistance, isJumpable);
    }

    private void ReleaseAttack() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("attack");
        }
    }

    private void Interact() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider2D detectedCollider = Physics2D.OverlapCircle(attackPoint.position, radiusAttack, isInteractable);
            if (detectedCollider != null) {
                if (detectedCollider.TryGetComponent(out IInteractable interactable)) {
                    interactable.Interact();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("DoubleJump")) {
            StartCoroutine(ActivateDoubleJump());
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("FallZone")) {
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator ActivateDoubleJump() {
        canDoubleJump = true;
        yield return new WaitForSeconds(10);
        canDoubleJump = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(attackPoint.position, radiusAttack);
    }

    // Se utiliza mediante evento de animación
    private void Attack() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, isDamageable);
        foreach (Collider2D collider in hitColliders) {
            LivesSystem livesSystemPlayer = collider.GetComponent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }
}
