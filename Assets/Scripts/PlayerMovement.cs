using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int jumpPower;

    public float riseDecceleration= 0.5f;
    private float yMovement=0; 
    private Rigidbody2D rb; 
    private Vector2 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if(yMovement>0){
            yMovement-=riseDecceleration * (Time.deltaTime * 500);
        }
        
        if (Input.GetAxis("Horizontal")==-1){
            MoveLeft();
        }
        else if(Input.GetAxis("Horizontal")==1){
            MoveRight();
        }
        
        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }

    void FixedUpdate(){
        rb.velocity=new Vector2((movementDirection*speed).x*(Time.deltaTime * 50),yMovement);
        movementDirection= new Vector2 (0,yMovement);
    }

    public void MoveLeft(){
    if (transform.rotation.y == -1 && movementDirection.x < 0) {
        transform.Rotate(new Vector3(0, -180, 0));
    }
    movementDirection= new Vector2 (-1,yMovement);
    }

    public void MoveRight(){
    if(transform.rotation.y == 0 && movementDirection.x > 0){
        transform.Rotate(new Vector3(0, 180, 0));
    }
    movementDirection= new Vector2 (1,yMovement);
    }

    public void Jump(){
        if(rb.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            yMovement=jumpPower;
        }
    }
}
