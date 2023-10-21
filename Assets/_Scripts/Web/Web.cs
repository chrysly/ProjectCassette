using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {

    [SerializeField] private GameObject coilPrefab;

    public List<Coil> coilList { get; private set; }
    public List<Wire> wireList { get; private set; }

    private static Web instance;
    public static Web Instance => instance;

    void Awake() {
        coilList = new List<Coil>();
        wireList = new List<Wire>();

        if (instance != this) {
            instance = this;
        } else Destroy(gameObject);
    }

    public void PlaceCoil(Vector2 coilPos) {
        GameObject coilGO = Instantiate(coilPrefab, coilPos, transform.rotation, transform);
        Coil coil = coilGO.GetComponentInChildren<Coil>(true);
        RegisterCoil(coil);
    }

    public void InitializeCoils() => coilList.ForEach(coil => coil.Init());

    public void RegisterCoil(Coil coil) => coilList.Add(coil);
    public void UnregisterCoil(Coil coil) => coilList.Remove(coil);

    public void RegisterWire(Wire wire) => wireList.Add(wire);
    public void UnregisterWire(Wire wire) => wireList.Remove(wire);
}
