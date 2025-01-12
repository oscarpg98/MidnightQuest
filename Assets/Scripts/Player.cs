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

    [Header("Combat System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damageAttack;
    [SerializeField] private LayerMask isDamageable;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Jump();
        ReleaseAttack();
    }

    private void Movement() {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * movementSpeed, rb.velocity.y);

        if (inputH != 0) {
            animator.SetBool("running", true);
            if (inputH > 0) {
                transform.eulerAngles = Vector3.zero;
            }
            else {
                transform.eulerAngles = new Vector3 (0, 180, 0);
            }
        }
        else {
            animator.SetBool("running", false);
        }
    }
    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded()) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }

    private bool Grounded() {
        return Physics2D.Raycast(feet.position, Vector3.down, rayDistance, isJumpable);
    }

    private void ReleaseAttack() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("attack");
        }
    }

    // Se utiliza mediante evento de animación
    private void Attack() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, isDamageable);
        foreach(Collider2D collider in hitColliders) {
            LivesSystem livesSystemPlayer = collider.GetComponent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(attackPoint.position, radiusAttack);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("FallZone")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
