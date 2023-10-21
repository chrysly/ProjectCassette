using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Habrador_Computational_Geometry;
using mattatz.Triangulation2DSystem;
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

    public void PlaceCoil(Triangle3 triangle2D) {
        Vector2 a = triangle2D.p1.ToMyVector2().ToVector2();
        Vector2 b = triangle2D.p2.ToMyVector2().ToVector2();
        Vector2 c = triangle2D.p3.ToMyVector2().ToVector2();
        
        GameObject coilGOA = Instantiate(coilPrefab, a, transform.rotation, transform);
        Coil coilA = coilGOA.GetComponentInChildren<Coil>(true);
        RegisterCoil(coilA);
        
        GameObject coilGOB = Instantiate(coilPrefab, a, transform.rotation, transform);
        Coil coilB = coilGOB.GetComponentInChildren<Coil>(true);
        RegisterCoil(coilB);
        
        GameObject coilGOC = Instantiate(coilPrefab, a, transform.rotation, transform);
        Coil coilC = coilGOC.GetComponentInChildren<Coil>(true);
        RegisterCoil(coilC);
    }

    public void InitializeCoils(Coil[] coils) {}

    public void RegisterCoil(Coil coil) => coilList.Add(coil);
    public void UnregisterCoil(Coil coil) => coilList.Remove(coil);

    public void RegisterWire(Wire wire) => wireList.Add(wire);
    public void UnregisterWire(Wire wire) => wireList.Remove(wire);
}
