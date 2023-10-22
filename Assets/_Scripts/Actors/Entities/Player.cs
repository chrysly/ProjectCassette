using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WebLocomotion))]
public class Player : Entity {

    public List<Coil> pathfound;
    public bool IsStopped => pathfound == null || pathfound.Count == 0;

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

    private void OnTriggerEnter2D(Collider2D collision) {
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null && entity is not Player) EndGame.Instance.FinishGame();
    }
}
