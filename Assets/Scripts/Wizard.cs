using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackTime;
    [SerializeField] private int damageAttack;
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack() {
        while (true) {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void LanzarBola() {
        Instantiate(fireball, spawnPoint.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerDetection")) {
            Debug.Log("Player detectado");
        }
        else if (other.CompareTag("PlayerHitbox")) {
            LivesSystem livesSystemPlayer = other.GetComponent<LivesSystem>();
            livesSystemPlayer.ReceiveDamage(damageAttack);
        }
    }
}
