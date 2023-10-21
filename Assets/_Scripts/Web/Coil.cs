using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coil : MonoBehaviour {

    [SerializeField] private GameObject wirePrefab;
    
    public Vector2 position => transform.position;

    private List<Wire> wires;

    void Awake() {
        wires = new List<Wire>();
        int radius = 100;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, radius, Vector2.one, 0.001f);
        for (int i = 0; i < Mathf.Min(hits.Length,4); i++) {
            var wire = Instantiate(wirePrefab, hits[i].point, transform.rotation, transform);
            ConnectWire(wire.GetComponentInChildren<Wire>(true));
        }
    }

    void OnDestroy() => Web.Instance.UnregisterCoil(this);

    private void ConnectWire(Wire wire) {
        if (wires.Count >= 4) return;
        wires.Add(wire);
        wire.OnWireCut += DetachWire;
        Web.Instance.RegisterWire(wire);
    }

    private void DetachWire(Wire wire) => wires.Remove(wire);
}
