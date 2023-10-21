using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coil : MonoBehaviour {

    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private int connections = 4;
    
    public Vector2 position => transform.position;

    private List<Wire> wires;

    void Awake() {
        wires = new List<Wire>();
    }

    public void Init() {
        int radius = 5;
        List<Collider2D> hits = new List<Collider2D>(Physics2D.OverlapCircleAll(position, radius));
        hits.Sort((coll1, coll2) => (int) (Vector3.Distance(coll1.transform.position, transform.position) -
            Vector3.Distance(coll2.transform.position, transform.position)));
        for (int i = 0; i < Mathf.Min(hits.Count, connections); i++) {
            var wire = Instantiate(wirePrefab, hits[i].transform.position, transform.rotation, transform);
            ConnectWire(wire.GetComponentInChildren<Wire>(true), hits[i].GetComponent<Coil>());
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
