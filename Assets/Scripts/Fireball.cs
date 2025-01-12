using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float shootingImpulse;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shootingImpulse, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}