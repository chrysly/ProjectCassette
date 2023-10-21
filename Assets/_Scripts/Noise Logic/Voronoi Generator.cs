using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class VoronoiGenerator {
    public List<Vector2> points;
    private int enclosureSize;
    private int gridSize;
    private int unitsPerCell;

    public VoronoiGenerator(int enclosureSize = 10, int gridSize = 10) {
        this.enclosureSize = enclosureSize;
        this.gridSize = gridSize;
        points = new List<Vector2>(gridSize * gridSize);
        GeneratePoints();
    }

    private void GeneratePoints() {
        unitsPerCell = enclosureSize / gridSize;
        Random random = new Random();
        
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                points.Add(new Vector2(i * unitsPerCell + (float) random.NextDouble() * unitsPerCell,
                    j * unitsPerCell + (float) random.NextDouble() * unitsPerCell));
            }
        }
    }
}
