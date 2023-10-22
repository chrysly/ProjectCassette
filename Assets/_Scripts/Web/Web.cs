using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Habrador_Computational_Geometry;
using mattatz.Triangulation2DSystem;
using UnityEngine;
using Random = System.Random;

public class Web : MonoBehaviour {

    [SerializeField] private GameObject coilPrefab;

    public PlayerController PlayerRef { get; private set; }

    public Dictionary<string, Coil> coilMap { get; private set; }
    public Dictionary<Coils, Wire> wireMap { get; private set; }

    private static Web instance;
    public static Web Instance => instance;

    void Awake() {

        if (instance != this) {
            instance = this;
        } else Destroy(gameObject);

        coilMap = new Dictionary<string, Coil>();
        wireMap = new Dictionary<Coils, Wire>();
    }

    public void PlaceEntity(Entity entity) {
        
    }

    public void PlacePlayer(Player player) {
        Coil[] coils = coilMap.Values.ToArray();
        player.Init(coils[UnityEngine.Random.Range(0, coils.Length)]);
        PlayerRef = player.GetComponent<PlayerController>();
    }

    private Coil PlaceCoil(Vector2 pos) {
        GameObject coilGO = Instantiate(coilPrefab, pos, transform.rotation, transform);
        Coil coil = coilGO.GetComponentInChildren<Coil>(true);
        RegisterCoil(coil);
        return coil;
    }

    public void PlaceCoils(Triangle3 triangle2D) {
        Vector2 a = triangle2D.p1.ToMyVector2().ToVector2();
        Vector2 b = triangle2D.p2.ToMyVector2().ToVector2();
        Vector2 c = triangle2D.p3.ToMyVector2().ToVector2();
        InitializeCoils(new[] { a, b, c });
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

    public Coil FindRandomValidCoil(Coil playerCoil) {
        List<Coil> neightborList = new List<Coil>();
        foreach (Wire wire in playerCoil.wires) {
            Coil coil1 = wire.coils.coil1;
            Coil coil2 = wire.coils.coil2;
            if (coil1 == playerCoil) {
                neightborList.Add(coil2);
            }
            else {
                neightborList.Add(coil1);
            }
        }

        bool foundValidCoil = false;
        int comparisonMax = coilMap.Values.Count;
        int comparisons = 0;    //Failsafe for infinite loop
        while (!foundValidCoil && comparisons < 1000) {
            Random random = new Random();
            int index = random.Next(0, comparisonMax);
            if (!neightborList.Contains(coilMap.Values.ToList()[index])) {
                return coilMap.Values.ToList()[index];
            }
        }

        //Brute force after 1000 failed comparisons
        foreach (Coil coil in coilMap.Values) {
            if (!neightborList.Contains(coil)) {
                return coil;
            }
        }

        return null;
    }

    public void RegisterCoil(Coil coil) => coilMap[coil.posKey] = coil;
    public void UnregisterCoil(Coil coil) => coilMap.Remove(coil.posKey);

    public void RegisterWire(Wire wire) => wireMap[wire.coils] = wire;
    public void UnregisterWire(Wire wire) => wireMap.Remove(wire.coils);
}
