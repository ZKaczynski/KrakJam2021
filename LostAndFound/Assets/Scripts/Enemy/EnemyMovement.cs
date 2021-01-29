using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    bool inChase = false;
    [SerializeField]
    private float speed = 0.5f;

    private Vector2 target;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {


            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

            Debug.Log("coll");

            target = other.transform.position;
        
    }


}
