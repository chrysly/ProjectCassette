using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    private static Transition instance;
    public static Transition Instance;

    void Awake() {
        if (instance != this) {
            instance = this;
            DontDestroyOnLoad(this);
        } else Destroy(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
