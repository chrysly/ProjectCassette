using System.Collections;
using System.Collections.Generic;
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
        GenerateWeb(noise.points);
    }

    private void GenerateWeb(List<Vector2> pointArr) {
        foreach (Vector2 pos in pointArr) {
            Web.Instance.PlaceCoil(pos);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Generate();
    }
}
