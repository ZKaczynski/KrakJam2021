using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField] private float speed = 2;


     private Quaternion lookRotation;
     private Vector3 direction;

    void Update() 
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed * inputX, speed * inputY,0);

        movement *= Time.deltaTime;

        transform.Translate(movement);





	}
}

