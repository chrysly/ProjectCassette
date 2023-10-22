using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameObject mouse;

    void Update() {
        bool leftMousePressed = Input.GetMouseButton(0);
        bool mouseMoved = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

        /*
        if (mouseMoved) {
            if (leftMousePressed) {

            } else {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos, 1, Vector2.one, 0f, 1 << 3);
                if (hit.Length > 0) mouse.transform.position = hit[0].point;
                else mouse.transform.position = mousePos;
            }
        }*/
    }

}
