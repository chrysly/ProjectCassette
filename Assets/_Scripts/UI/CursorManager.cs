using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {

    [SerializeField] private Sprite normalTex;
    [SerializeField] private Sprite selectTex;
    [SerializeField] private Sprite attackSilkTex;
    [SerializeField] private Sprite attackGoldTex;

    private SpriteRenderer spr;

    private Vector2 baseScale; 

    public enum CursorType {
        Normal,
        Select,
        Attack,
        AttackGold,
    }

    private static CursorManager instance;
    public static CursorManager Instance => instance;

    void Awake() {
        if (instance != this) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);

        Cursor.visible = false;
        baseScale = transform.localScale;
        SetCursorType(CursorType.Normal);
    }

    void Update() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = baseScale + Vector2.one * Mathf.PingPong(Time.time, 0.05f);
    }

    public void SwitchCursorType(CursorType cursorType) => StartCoroutine(SwitchCursor(cursorType));

    private IEnumerator SwitchCursor(CursorType cursorType) {
        StopAllCoroutines();
        while ((Vector2) transform.localScale != Vector2.zero) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, Vector2.zero, Time.unscaledDeltaTime * 5);
            yield return null;
        } SwitchCursorType(cursorType);
        while ((Vector2) transform.localScale != baseScale) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, baseScale, Time.unscaledDeltaTime * 5);
            yield return null;
        }
    }

    private void SetCursorType(CursorType cursorType) {
        switch (cursorType) {
            case CursorType.Normal:
                spr.sprite = normalTex;
                break;
            case CursorType.Select:
                spr.sprite = selectTex;
                break;
            case CursorType.Attack:
                spr.sprite = attackSilkTex;
                break;
            case CursorType.AttackGold:
                spr.sprite = attackGoldTex;
                break;
        }
    }
}
