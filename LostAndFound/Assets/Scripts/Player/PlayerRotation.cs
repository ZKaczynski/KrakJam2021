using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public class PlayerRotation : MonoBehaviour
    {

        [SerializeField]
        private Fov fov;

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            fov.SetAimDirection((transform.position - mousePos).normalized);
            //print(( mousePos- transform.position).normalized);

            Vector3 perpendicular = Vector3.Cross(transform.position - mousePos, Vector3.forward);
           fov.SetOrigin(transform.position);
            



            transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
        }
    }
}