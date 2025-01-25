using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlatforms : MonoBehaviour {
    public Vector2 pointA = new Vector2(-2, 0);
    public Vector2 pointB = new Vector2(2, 0);
    public float speed = 2f;

    private bool movingToB = true;
    private TilemapCollider2D tilemapCollider;

    private void Start() {
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void Update() {
        Vector3 target = movingToB ? (Vector3)pointB : (Vector3)pointA;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f) {
            movingToB = !movingToB;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerHitbox")) {
            // Hace que el jugador sea hijo de la plataforma temporalmente para que se mueva con la plataforma
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // Elimina la relación del player con la plataforma móvil
        if (collision.gameObject.CompareTag("PlayerHitbox") && collision.transform.parent != null) {
            collision.transform.SetParent(null);
        }
    }
}
