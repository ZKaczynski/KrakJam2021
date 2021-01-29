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
        inLight = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        target = other.transform.position;
        inLight = false;
    }

}
