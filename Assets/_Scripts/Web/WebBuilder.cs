using System.Collections;
using System.Collections.Generic;
using Habrador_Computational_Geometry;
using mattatz.Triangulation2DSystem;
using UnityEngine;

public class WebBuilder : MonoBehaviour {

    [SerializeField] private int webSize;
    [SerializeField] private int webDensity;
    [SerializeField] private VoronoiController voronoi;
    private int runs = 0;
    /// <summary>
    /// Put code to start web here using the GenerateWeb method;
    /// </summary>
    private void Generate() {
        VoronoiGenerator noise = new VoronoiGenerator(webSize, webDensity);
        Polygon2D polygon = Polygon2D.ConvexHull(noise.points.ToArray());
        Triangulation2D triangulation = new Triangulation2D(polygon, 22.5f);
        Mesh mesh = triangulation.Build();
        var GO = new GameObject();
        var f = GO.AddComponent<MeshFilter>();
        GO.AddComponent<MeshRenderer>();
        f.mesh = mesh;

        GenerateWeb(triangulation.Triangles);
    }

    private void GenerateWeb(List<Triangle3> pointArr) {
        foreach (Triangle3 tri in pointArr) {
            //Pass TRIANGLE2D into web, initialize coils using points and
            Web.Instance.PlaceCoils(pos);
        }
        //Web.Instance.InitializeCoils();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Generate();
    }
}
