using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{


    public float speed = 10;

    void Update() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed * inputX, speed * inputY,0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
}
