using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
}
