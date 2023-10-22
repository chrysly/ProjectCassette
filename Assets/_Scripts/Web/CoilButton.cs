using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilButton : MonoBehaviour {

    private Vector2 baseScale;
    private Vector2 targetScale;

    void Awake() {
        baseScale = transform.localScale;
        targetScale = baseScale;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<CursorManager>()) {
            CursorManager.Instance.SwitchCursorType(CursorManager.CursorType.Select);
            targetScale = 2f * baseScale;
            StopAllCoroutines();
            StartCoroutine(Animate());
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.GetComponent<CursorManager>()) {
            CursorManager.Instance.SwitchCursorType(CursorManager.CursorType.Normal);
            targetScale = baseScale;
            StopAllCoroutines();
            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate() {
        while ((Vector2) transform.localScale != 1.25f * targetScale) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, targetScale, Time.deltaTime * 10);
            yield return null;
        } while ((Vector2) transform.localScale != targetScale) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, targetScale, Time.deltaTime * 5);
            yield return null;
        }
    }
}
