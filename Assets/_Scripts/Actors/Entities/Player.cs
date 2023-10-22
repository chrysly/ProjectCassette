using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WebLocomotion))]
public class Player : Entity {

    public List<Coil> pathfound;

    protected override void Awake() {
        base.Awake();
        locomotion.OnTargetReached += Player_OnTargetReached;
    }

    private void Player_OnTargetReached(Coil target) {
        AdvanceMotion();
        CurrCoil = target;
    }

    public void BeginNewPath(List<Coil> path) {
        if (path == null || path.Count == 0) return;
        pathfound = path;
        AdvanceMotion();
    }

    private void AdvanceMotion() {
        if (pathfound.Count == 0) return;
        locomotion.SetTargetCoil(pathfound[0]);
        pathfound.RemoveAt(0);
    }
}
