using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebLocomotion))]
public class Roamer : Entity {

    private enum Action {
        Wait = 0,
        Move= 1,
        Act = 2,
    }

    protected override void Awake() {
        base.Awake();
        locomotion.OnTargetReached += Roamer_OnTargetReached;
    }

    public override void Init(Coil startingCoil) {
        base.Init(startingCoil);
        StartCoroutine(ChooseAction());
    }

    private void Roamer_OnTargetReached(Coil target) {
        CurrCoil = target;
        StartCoroutine(ChooseAction());
    }

    public IEnumerator ChooseAction() {
        var choice = Random.Range(0, 3);
        switch ((Action) choice) {
            case Action.Wait: /// Action
                yield return new WaitForSeconds(Random.Range(0f, 2f));
                StartCoroutine(ChooseAction());
                break;
            case Action.Move:
                Coil target = ChooseTarget(CurrCoil);
                locomotion.SetTargetCoil(target);
                break;
            case Action.Act: /// Move
                /// Break a nearby web;
                StartCoroutine(ChooseAction());
                break;
        }
    }

    protected Coil ChooseTarget(Coil currCoil) {
        var coilList = currCoil.wires.Select(wire => (new[] { wire.coils.coil1, wire.coils.coil2 }).First(coil => coil != currCoil)).ToList();
        var targetCoil = coilList[Random.Range(0, coilList.Count)];
        return targetCoil;
    }
}
