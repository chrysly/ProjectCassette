using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebLocomotion))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour {

    public readonly int MaxHP;
    public int HP { get; protected set; }
    public Coil CurrCoil { get; protected set; }

    protected SpriteRenderer spr;
    protected Color baseColor;
    protected WebLocomotion locomotion;

    protected virtual void Awake() {
        HP = MaxHP;
        locomotion = GetComponent<WebLocomotion>();
        spr = GetComponent<SpriteRenderer>();
    }

    public virtual void Init(Coil startingCoil) {
        CurrCoil = startingCoil;
        locomotion.Init(this, startingCoil);
    }

    public void SimulateDamage(int dmgVal) => StartCoroutine(_SimulateDamage(dmgVal));

    private IEnumerator _SimulateDamage(int dmgVal) {
        while (spr.color != Color.red) {
            spr.color = Vector4.MoveTowards(spr.color, Color.red, Time.deltaTime * 2);
            yield return null;
        }
        HP -= dmgVal;
        if (HP <= 0) {
            Destroy(gameObject);
        } else {
            while (spr.color != baseColor) {
                spr.color = Vector4.MoveTowards(spr.color, baseColor, Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}
