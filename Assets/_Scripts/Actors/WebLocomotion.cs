using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WebLocomotion : MonoBehaviour {

    [SerializeField] private float travelSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool animated;

    private Entity entity;
    private Animator animator;
    
    private Coil targetCoil;
    private Quaternion direction;

    public event System.Action<Coil> OnTargetReached;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Init(Entity entity, Coil startingCoil) {
        this.entity = entity;
        transform.position = startingCoil.position;
    }

    void Update() {

        if (targetCoil != null) {
            if ((Vector2) transform.position != targetCoil.position) {
                transform.position = Vector2.MoveTowards(transform.position, targetCoil.position, travelSpeed * Time.deltaTime);
            } else {
                SetAnimationParam("IsMoving", false);
                OnTargetReached?.Invoke(targetCoil);
            }
        } transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, Time.deltaTime * rotationSpeed);
    }

    public void SetAnimationParam(string paramName, bool paramValue) {
        if (!animated) return;
        animator.SetBool(paramName, paramValue);
    }

    public void SetTargetCoil(Coil targetCoil) {
        this.targetCoil = targetCoil;
        SetDirection(targetCoil.position - entity.CurrCoil.position);
        SetAnimationParam("IsMoving", true);
    }

    public void SetDirection(Vector2 direction) {
        this.direction = Quaternion.LookRotation(Vector3.forward, direction);
    }
}