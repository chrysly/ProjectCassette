using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebNode {
    public Coil coil;
    public WebNode prev;
    public Vector3 location;

    /// <summary>
    /// Distance from starting cell node.
    /// </summary>
    public float gCost;
    /// <summary>
    /// Distance from ending cell node (Heuristic).
    /// </summary>
    public float hCost;

    public WebNode(Coil coil) {
        this.coil = coil;
        location = coil.transform.position;
    }

    /// <summary>
    /// Sum of G & H costs.
    /// </summary>
    public float fCost {
        get {
            return gCost + hCost;
        }
    }
}
