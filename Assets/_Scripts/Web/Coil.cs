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
    public string posKey => transform.position.Precise();

    private List<Wire> wires;
    public bool Full => wires.Count >= connections;

    void Awake() {
        wires = new List<Wire>();
    }

    void OnDestroy() => Web.Instance.UnregisterCoil(this);

    public void ConnectWire(Wire wire) {
        wires.Add(wire);
        wire.OnWireCut += DetachWire;
    }

    public void ConnectCoil(Coil coil) {
        if (Full || coil.Full) return;
        var wireGO = Instantiate(wirePrefab, position, transform.rotation, transform);
        Wire wire = wireGO.GetComponentInChildren<Wire>(true);
        wire.Init(this, coil);
        ConnectWire(wire);
        coil.ConnectWire(wire);
    }

    private void DetachWire(Wire wire) {
        wires.Remove(wire);
        DeleteIfDisconnected();
    }

    public void DeleteIfDisconnected() {
        if (wires.Count == 0) Destroy(gameObject);
    }
}
