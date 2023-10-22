using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    private static EndGame instance;
    public static EndGame Instance => instance;

    bool end;

    void Awake() {
        if (instance != this) {
            instance = this;
        } else Destroy(this);
    }

    public void FinishGame() {
        SceneManager.LoadScene(0);
    }
}
