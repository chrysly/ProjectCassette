using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    private LineRenderer lr;
    public event System.Action<Wire> OnWireCut;

    public Coils coils { get; private set; }
    public float Distance => Vector2.Distance(coils.coil1.position, coils.coil2.position); 

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void OnDestroy() {
        Web.Instance.UnregisterWire(this);
        OnWireCut?.Invoke(this);
    }

    public void Init(Coil coil1, Coil coil2) {
        coils = new Coils(coil1, coil2);
        lr.SetPositions(new Vector3[] { coil1.position, coil2.position } );
        Web.Instance.RegisterWire(this);
    }
}

public struct Coils {
    public Coil coil1;
    public Coil coil2;

    public Coils(Coil coil1, Coil coil2) {
        this.coil1 = coil1;
        this.coil2 = coil2;
    }
}
