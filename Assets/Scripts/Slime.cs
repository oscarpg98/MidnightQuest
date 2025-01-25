using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {
    protected override void Start() {
        base.Start();
        StartCoroutine(base.Patrol());
    }

    protected override IEnumerator Attack() {
        Debug.Log("Slime attacking!");
        return null;
    }
}
