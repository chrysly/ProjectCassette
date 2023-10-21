using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBuilder : MonoBehaviour {

    /// <summary>
    /// Put code to start web here using the GenerateWeb method;
    /// </summary>
    private void Generate() {

        /// GenerateWeb(Vector2ArraySmth);
    }

    private void GenerateWeb(Vector2[] pointArr) {
        foreach (Vector2 pos in pointArr) {
            Web.Instance.PlaceCoil(pos);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) Generate();
    }
}
