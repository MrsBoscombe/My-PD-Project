using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Why not just create a Vector3 here?
    private Vector3 movementVector3;
    public float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        movementVector3 = new Vector3(movementX, 0.0f, movementY);

    }

    private void FixedUpdate(){
        //Vector3 movementVector
        rb.AddForce(movementVector3 * speed);    


    }
    
}
