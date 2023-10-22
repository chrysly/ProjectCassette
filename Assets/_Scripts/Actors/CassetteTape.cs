using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CassetteTape : MonoBehaviour {
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        SetVelocity();
    }

    // Update is called once per frame
    private void SetVelocity() {
        Random rand = new System.Random();
        if (rand.Next(0, 2) == 1) transform.right += new Vector3(0, (float) rand.NextDouble() * 1 - 0.5f, (float) rand.NextDouble() * 1 - 0.5f);
        rb.velocity = transform.right * bulletSpeed;
    }
}
