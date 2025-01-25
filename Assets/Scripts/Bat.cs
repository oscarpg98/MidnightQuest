using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy {

    protected override void Start() {
        base.Start();
        StartCoroutine(base.Patrol());
    }
    protected override IEnumerator Attack() {
        Debug.Log("Bat attacking!");
        return null;
    }

    private void Update() {
        transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
    }
}
