using System.Linq;
using System.Collections;
using System.Collections.Generic;
using mattatz.Triangulation2DSystem;
using Unity.VisualScripting;
using UnityEngine;

public class Coil : MonoBehaviour {

    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private int connections = 4;
    
    public Vector2 position => transform.position;

    private List<Wire> wires;

    private Coil[] connectionPoints;

    void Awake() {
        wires = new List<Wire>();
    }

    public void Init(Coil[] connectionPoints) {
        for (int i = 0; i < connectionPoints.Length; i++) {
            var wire = Instantiate(wirePrefab, connectionPoints[i].transform.position, transform.rotation, transform);
            ConnectWire(wire.GetComponentInChildren<Wire>(true), connectionPoints[i]);
        }
    }

    void OnDestroy() => Web.Instance.UnregisterCoil(this);

    private void ConnectWire(Wire wire, Coil coil) {
        if (wires.Count >= connections) return;
        wires.Add(wire);
        wire.Init(this, coil);
        wire.OnWireCut += DetachWire;
        Web.Instance.RegisterWire(wire);
    }

    private void DetachWire(Wire wire) => wires.Remove(wire);
}
