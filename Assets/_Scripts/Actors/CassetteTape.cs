using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteTape : MonoBehaviour {
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        SetVelocity();
    }

    // Update is called once per frame
    private void SetVelocity() {
        rb.velocity = Vector3.right * bulletSpeed;
    }
}
