using System.Collections;
using System.Collections.Generic;
using mattatz.Triangulation2DSystem;
using UnityEngine;

public class WebBuilder : MonoBehaviour {

    [SerializeField] private int webSize;
    [SerializeField] private int webDensity;
    private int runs = 0;
    /// <summary>
    /// Put code to start web here using the GenerateWeb method;
    /// </summary>
    private void Generate() {
        VoronoiGenerator noise = new VoronoiGenerator(webSize, webDensity);
        Polygon2D polygon = Polygon2D.Contour(noise.points.ToArray());
        Triangulation2D triangulation = new Triangulation2D(polygon, 22.5f);
        
        GenerateWeb(triangulation.Triangles);
    }

    private void GenerateWeb(Triangle2D[] pointArr) {
        foreach (Triangle2D pos in pointArr) {
            //Pass TRIANGLE2D into web, initialize coils using points and 
            Web.Instance.PlaceCoil(pos);
        }
        //Web.Instance.InitializeCoils();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Generate();
    }
}
