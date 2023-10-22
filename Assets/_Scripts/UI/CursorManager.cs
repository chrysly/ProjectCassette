using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    [SerializeField] private Sprite normalTex;
    [SerializeField] private Sprite selectTex;
    [SerializeField] private Sprite attackSilkTex;
    [SerializeField] private Sprite attackGoldTex;

    private Coroutine cursorSwap;
    private SpriteRenderer spr;
    private float cursorSpin;

    private Vector2 baseScale; 

    public enum CursorType {
        Normal = 0,
        Select = 1,
        Attack = 2,
        AttackGold = 3,
    } private CursorType cursorType;

    private static CursorManager instance;
    public static CursorManager Instance => instance;

    void Awake() {
        if (instance != this) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(gameObject);

        Cursor.visible = false;
        spr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
        SetCursorType(CursorType.Normal);
    }

    void Update() {
        /// Follow system cursor;
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /// Animations;
        if (cursorSwap != null) return;
        if ((int) cursorType > 1 && cursorSpin > 0) {
            //transform.localScale = Vector2.MoveTowards(transform.localScale, Time.deltaTime * cursorSpin * Vector2.one + baseScale, Time.deltaTime * 2);
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * cursorSpin);
            cursorSpin = Mathf.MoveTowards(cursorSpin, 0, Time.deltaTime);
        } transform.localScale = baseScale + Vector2.one * Mathf.PingPong(Time.time / 5, 0.1f);
    }

    public void AddCursorSpin(float spin) => cursorSpin += spin;

    public void SwitchCursorType(CursorType cursorType) {
        StopAllCoroutines();
        cursorSwap = StartCoroutine(SwitchCursor(cursorType));
    }

    private IEnumerator SwitchCursor(CursorType cursorType) {
        while ((Vector2) transform.localScale != baseScale * 0.65f) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, 0.65f * baseScale, Time.unscaledDeltaTime * 7.25f);
            yield return null;
        } SetCursorType(cursorType);
        while ((Vector2) transform.localScale != baseScale * 1.1f) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, 1.1f * baseScale, Time.unscaledDeltaTime * 5.5f);
            yield return null;
        } while ((Vector2) transform.localScale != baseScale) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, baseScale, Time.unscaledDeltaTime * 3.75f);
            yield return null;
        } this.cursorType = cursorType;
        cursorSwap = null;
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
