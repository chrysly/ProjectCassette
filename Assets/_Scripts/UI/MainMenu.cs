using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        // Transition.Instance.
    }

    public void Options() {

    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.Exit(0);
        #else
            Application.Quit();
        #endif
    }
}
