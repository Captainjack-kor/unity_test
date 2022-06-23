using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    private float moveSpeed = 0.5f;

    void Update () {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            // transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if(Input.GetAxisRaw("Horizontal") != 0) {
                transform.Translate(Input.GetAxisRaw("Horizontal") * moveSpeed, 0, -1);
            } else if(Input.GetAxisRaw("Vertical") != 0) {
                transform.Translate(0, Input.GetAxisRaw("Vertical") * moveSpeed, -1);
            }
        }

        // if (Input.GetAxis("Mouse ScrollWheel") != 0) {
        //     transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        // }
    }

}
