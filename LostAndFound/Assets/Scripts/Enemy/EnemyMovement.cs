using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    bool inLight = false;
    [SerializeField]
    private float speed = 0.5f;

    private Vector2 target;

    void Start()
    {
        target = transform.position;
    }


    void Update()
    {
        if (target != null && inLight == false)
        {


            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

      //  RaycastHit2D hit = Physics2D.Raycast(transform.position, other.gameObject.transform.position);

      //  if (hit.collider == null)
      //  {
            inLight = true;
      //  }

        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

       // RaycastHit2D hit = Physics2D.Raycast(transform.position, other.gameObject.transform.position);
       // Debug.Log(hit)

        //if (hit.collider == null)
       // {

            target = other.transform.position;
            inLight = false;

       // }
    }

}
