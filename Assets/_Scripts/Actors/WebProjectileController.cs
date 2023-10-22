using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebProjectileController : MonoBehaviour {
    [SerializeField] private GameObject webProjectilePrefab;
    [SerializeField] private int maxFireRate = 50;
    [SerializeField] private int fireRateGrowth = 2;
    [SerializeField] private float fireUptimeSpeed = 0.1f;
    [SerializeField] private Transform cassette;

    private Player player;

    private IEnumerator action;
    private IEnumerator fireAction;
    private int fireRate = 0;

    private int idleCounter = 0;
    private int idleMax = 5;
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsStopped) return;
        CalculateFireRate();
        FireWeb();
    }

    private void FireWeb() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        cassette.transform.up = aimDirection;
        if (fireRate > 0) {
            if (fireAction == null) {
                fireAction = FireAction(aimDirection);
                StartCoroutine(fireAction);
            }
        }
    }

    private IEnumerator FireAction(Vector2 aimDirection) {
        Instantiate(webProjectilePrefab, cassette.position, Quaternion.LookRotation(Vector3.forward, aimDirection));
        yield return new WaitForSeconds(1f / fireRate);
        fireAction = null;
        yield return null;
    }

    private void CalculateFireRate() {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            idleCounter = 0;
            if (action == null && fireRate < maxFireRate) {
                action = ReelAction();
                StartCoroutine(action);
            }
        }
        else {
            if (fireRate > 0 && action == null) {
                action = UnreelAction();
                StartCoroutine(action);
            }
        }
        Debug.Log("Firerate: " + fireRate);
    }

    private IEnumerator ReelAction() {
        fireRate += fireRateGrowth;
        yield return new WaitForSeconds(fireUptimeSpeed / 5);
        action = null;
        yield return null;
    }

    private IEnumerator UnreelAction() {
        if (idleCounter < idleMax) {
            idleCounter++;
            yield return new WaitForSeconds(fireUptimeSpeed);
        }
        else {
            fireRate -= fireRateGrowth;
            yield return new WaitForSeconds(fireUptimeSpeed / 2);
        }
        action = null;
        if (fireRate < 0) fireRate = 0;
        yield return null;
    }
}
