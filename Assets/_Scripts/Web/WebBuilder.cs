using System.Collections;
using System.Collections.Generic;
using Habrador_Computational_Geometry;
using mattatz.Triangulation2DSystem;
using UnityEngine;

public class WebBuilder : MonoBehaviour {

    [SerializeField] private GameObject player;
    [Space]
    [SerializeField] private GameObject roamer;
    [SerializeField] private int roamerCount;
    [Space]
    [SerializeField] private GameObject boss;
    [Space]
    [SerializeField] private int webSize;
    [SerializeField] private int webDensity;
    [SerializeField] private VoronoiController voronoi;

    /// <summary>
    /// Put code to start web here using the GenerateWeb method;
    /// </summary>
    private void Generate() {
        // VoronoiGenerator noise = new VoronoiGenerator(webSize, webDensity);
        // Polygon2D polygon = Polygon2D.ConvexHull(noise.points.ToArray());
        // Triangulation2D triangulation = new Triangulation2D(polygon, 22.5f);
        // Mesh mesh = triangulation.Build();
        // var GO = new GameObject();
        // var f = GO.AddComponent<MeshFilter>();
        // GO.AddComponent<MeshRenderer>();
        // f.mesh = mesh;

        List<Triangle3> triangulation = voronoi.Generate();

        GenerateWeb(triangulation);
    }

    private void GenerateWeb(List<Triangle3> pointArr) {
        foreach (Triangle3 tri in pointArr) {
            //Pass TRIANGLE2D into web, initialize coils using points and
            Web.Instance.PlaceCoils(tri);
        } PlaceEntities();
    }

    private void PlaceEntities() {
        /// Place Player;
        GameObject playerGO = Instantiate(player, transform.position, transform.rotation);
        Web.Instance.PlacePlayer(playerGO.GetComponent<Player>());
        /// Place Boss;
        Instantiate(boss, Boss.RandomizeBossSpawn(), transform.rotation);
        /// Place Roamers;
        for (int i = 0; i < roamerCount; i++) {
            GameObject roamerGO = Instantiate(roamer, transform.position, transform.rotation, Web.Instance.transform);
            Web.Instance.PlaceEntity(roamerGO.GetComponent<Roamer>());
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Generate();
    }
}
