using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    public Sprite doorClosedSprite;
    public Sprite doorOpenSprite;
    private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;
    private bool isDoorOpen = false;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer.sprite = doorClosedSprite;
    }
    public void Interact() {
        ToggleDoor();
    }

    private void ToggleDoor() {
        if (isDoorOpen) {
            spriteRenderer.sprite = doorClosedSprite;
            doorCollider.enabled = true;
        }
        else {
            spriteRenderer.sprite = doorOpenSprite;
            doorCollider.enabled = false;
        }
        isDoorOpen = !isDoorOpen;
    }
}
