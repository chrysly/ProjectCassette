using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WireDestroyPS : MonoBehaviour {

    private ParticleSystem ps;

    void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    public void Init(LineRenderer lr) {
        Mesh mesh = new Mesh();
        lr.BakeMesh(mesh);
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        shape.mesh = mesh;
        ps.Play();
    }
}
