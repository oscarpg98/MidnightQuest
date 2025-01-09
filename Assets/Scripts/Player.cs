using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private float inputH;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Jump();
        Attack();
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }

    private void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("attack");
        }
    }


}
