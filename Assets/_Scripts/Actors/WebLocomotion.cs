using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WebLocomotion : MonoBehaviour {

    [SerializeField] private float travelSpeed;
    private Coil targetCoil;

    public event System.Action<Coil> OnTargetReached;

    public void Init(Coil startingCoil) {
        transform.position = startingCoil.position;
    }

    void Update() {

        if (targetCoil != null) {
            if ((Vector2) transform.position != targetCoil.position) {
                transform.position = Vector2.MoveTowards(transform.position, targetCoil.position, travelSpeed * Time.deltaTime);
            } else {
                OnTargetReached?.Invoke(targetCoil);
            }
        }
    }

    public void SetTargetCoil(Coil targetCoil) {
        this.targetCoil = targetCoil;
    }
}