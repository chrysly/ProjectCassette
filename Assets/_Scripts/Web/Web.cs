using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Habrador_Computational_Geometry;
using mattatz.Triangulation2DSystem;
using UnityEngine;

public class Web : MonoBehaviour {

    [SerializeField] private GameObject coilPrefab;

    public Dictionary<string, Coil> coilMap { get; private set; }
    public Dictionary<Coils, Wire> wireMap { get; private set; }

    private static Web instance;
    public static Web Instance => instance;

    void Awake() {
        coilMap = new Dictionary<string, Coil>();
        wireMap = new Dictionary<Coils, Wire>();

        if (instance != this) {
            instance = this;
        } else Destroy(gameObject);
    }

<<<<<<< HEAD
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
=======
    private Coil PlaceCoil(Vector2 pos) {
        GameObject coilGO = Instantiate(coilPrefab, pos, transform.rotation, transform);
        Coil coil = coilGO.GetComponentInChildren<Coil>(true);
        RegisterCoil(coil);
        return coil;
    }

    public void PlaceCoils(Triangle2D triangle2D) {
        Vector2 a = triangle2D.a.Coordinate;
        Vector2 b = triangle2D.b.Coordinate;
        Vector2 c = triangle2D.c.Coordinate;
        InitializeCoils(new[] { a, b, c });
>>>>>>> main
    }

    public void InitializeCoils(Vector2[] posArr) {
        if (posArr == null || posArr.Length == 0) throw new System.Exception("Invalid Voronoi Triangle;");
        Coil[] coilArr = new Coil[posArr.Length];
        for (int i = 0; i < posArr.Length; i++) {
            if (posArr[i] == null) continue;
            string posKey = posArr[i].Precise();
            try {
                coilArr[i] = coilMap[posKey];
            } catch (KeyNotFoundException) {
                coilArr[i] = PlaceCoil(posArr[i]);
            }
        } AttachTriangle(coilArr);
    }

    private void AttachTriangle(Coil[] coilArr) {
        if (coilArr.Length == 1) Debug.Log("oof"); 
        for (int i = 0; i < coilArr.Length; i++) {
            int clampedIndex = i + 1 == coilArr.Length ? 0 : i + 1;
            if (coilArr[i] == null || coilArr[clampedIndex] == null
                || wireMap.ContainsKey(new Coils(coilArr[i], coilArr[clampedIndex]))) continue;
            coilArr[i].ConnectCoil(coilArr[clampedIndex]);
        } foreach (Coil coil in coilArr) coil.DeleteIfDisconnected();
    }

    public void RegisterCoil(Coil coil) => coilMap[coil.posKey] = coil;
    public void UnregisterCoil(Coil coil) => coilMap.Remove(coil.posKey);

    public void RegisterWire(Wire wire) => wireMap[wire.coils] = wire;
    public void UnregisterWire(Wire wire) => wireMap.Remove(wire.coils);
}
