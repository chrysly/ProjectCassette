using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebLocomotion))]
public class Boss : Entity {

    [SerializeField] private float travelSpeed;

    protected override void Awake() {
        base.Awake();
    }

    void Start() {
        //locomotion.SetAnimationParam("IsMoving", true);
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, Web.Instance.PlayerRef.transform.position, Time.deltaTime * travelSpeed);
        locomotion.SetDirection(Web.Instance.PlayerRef.transform.position - transform.position);
    }

    public static Vector2 RandomizeBossSpawn() {
        int scenario = Random.Range(0, 4);
        var res = scenario switch {
            1 => new Vector2(-100, Random.Range(-50, 50)),
            2 => new Vector2(Random.Range(-50, 50), 100),
            3 => new Vector2(Random.Range(-50, 50), -100),
            _ => new Vector2(100, Random.Range(-50, 50)),
        }; return res;
    }
}