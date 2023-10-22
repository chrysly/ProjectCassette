using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {

    private CinemachineVirtualCamera vc;

    void Awake() {
        vc = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        vc.Follow = transform;
        vc.LookAt = transform;
    }
}
