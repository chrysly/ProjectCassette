using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CassetteTape : MonoBehaviour {
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float spread = 0.2f;
    private Rigidbody rb;
    private bool collided = false;
    void Start() {
        rb = GetComponent<Rigidbody>();
        SetVelocity();
        StartCoroutine(DestroyAction());
    }

    // Update is called once per frame
    private void SetVelocity() {
        Random rand = new System.Random();
        if (rand.Next(0, 2) == 1) transform.right += new Vector3(0, (float) rand.NextDouble() * (spread + spread) - spread, (float) rand.NextDouble() * 1 - 0.5f);
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (collided && other.transform.GetComponent<Boss>() != null) {
            collided = true;
            other.transform.GetComponent<Boss>().HP -= 2;
            
            Destroy(transform.GetChild(1));
            Destroy(transform.GetChild(2));
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator DestroyAction() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        yield return null;
    }
}
