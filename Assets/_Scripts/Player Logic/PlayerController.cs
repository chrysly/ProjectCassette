using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {

    private Player player;
    public Coil CurrCoil => player.CurrCoil;

    private CinemachineVirtualCamera vc;
    private PlayerPathfinding ppf;

    void Awake() {
        vc = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        vc.Follow = transform;
        vc.LookAt = transform;

        player = GetComponent<Player>();
    }

    void Start() {
        ppf = new PlayerPathfinding(Web.Instance.coilMap.Values.ToList());
    }

    public void DefineNewPath(Coil targetCoil) {
        List<Coil> path = ppf.FindPath(player.CurrCoil, targetCoil);
        player.BeginNewPath(path);
    }
}
