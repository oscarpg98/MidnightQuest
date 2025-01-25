using System.Collections;
using UnityEngine;

public class Wizard : Enemy {
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackTime;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private bool isAttacking = false;
    private Transform playerTransform;

    protected override void Start() {
        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("PlayerHitbox");
        if (player != null) {
            playerTransform = player.transform;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag("PlayerDetection")) {
            Debug.Log("Jugador detectado por el Wizard.");
            if (!isAttacking) {
                isAttacking = true;
                StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("PlayerDetection")) {
            Debug.Log("El jugador ha salido del área de detección.");
            isAttacking = false;
        }
    }

    protected override IEnumerator Attack() {
        while (isAttacking) {
            FacePlayer();
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(attackTime);
        }
    }

    // Se llama mediante un evento de animación
    private void LanzarBola() {
        GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);
        Vector2 fireballDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        fireball.GetComponent<Fireball>().SetDirection(fireballDirection);
        AudioManager.Instance.PlaySoundEffect(fireballSound);
    }



    private void FacePlayer() {
        if (playerTransform == null) return;

        if (playerTransform.position.x > transform.position.x) {
            // Jugador a la derecha
            transform.localScale = new Vector3(1, 1, 1);
            spawnPoint.localPosition = new Vector3(Mathf.Abs(spawnPoint.localPosition.x), spawnPoint.localPosition.y, spawnPoint.localPosition.z);
        }
        else {
            // Jugador a la izquierda
            transform.localScale = new Vector3(-1, 1, 1);
            spawnPoint.localPosition = new Vector3(-Mathf.Abs(spawnPoint.localPosition.x), spawnPoint.localPosition.y, spawnPoint.localPosition.z);
        }
    }
}
