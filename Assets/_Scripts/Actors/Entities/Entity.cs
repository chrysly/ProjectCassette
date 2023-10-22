using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebLocomotion))]
public abstract class Entity : MonoBehaviour {

    public int HP { get; protected set; }
    public Coil CurrCoil { get; protected set; }

    protected WebLocomotion locomotion;

    protected virtual void Awake() {
        locomotion = GetComponent<WebLocomotion>();
    }

    public virtual void Init(Coil startingCoil) {
        CurrCoil = startingCoil;
        locomotion.Init(startingCoil);
    }
}
