using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private int coil1;
    [SerializeField] private int coil2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            RunAStarAlgo();
        }
    }

    void RunAStarAlgo() {
        List<Coil> coilValues = Web.Instance.coilMap.Values.ToList();
        if (coilValues.Count == 0) Debug.Log("Empty web");
        PlayerPathfinding pathfinding = new PlayerPathfinding(coilValues);
        List<Coil> coils = new List<Coil>();
        coils = pathfinding.FindPath(coilValues[coil1], coilValues[coil2]);
        coilValues[coil1].transform.DOScale(15f, 1f);
        coilValues[coil2].transform.DOScale(15f, 1f);
        if (coils == null) {
            Debug.Log("Coil list is empty");
            return;
        }
        foreach (Coil coil in coils) {
            coil.transform.DOScale(10f, 0.3f);
        }
    }
}
