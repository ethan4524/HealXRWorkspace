using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMotion : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float maxSpeed = 1;
    public bool moveA = false;
    public bool moveB = false;


    public void Update() 
    {

        if (moveA == true)
        {
            moveB = false;
            var change = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, change);
            Debug.Log("MOVING TO B");
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f) {
                moveA = false;
                Debug.Log("STOPPING");
            }
        }  

        if (moveB == true)
        {
            moveA = false;
            var change2 = maxSpeed * Time.deltaTime;
            Debug.Log("MOVING TO A");
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, change2);
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f) {
                moveB = false;
                Debug.Log("STOPPING");
            }
        }

    }
}
