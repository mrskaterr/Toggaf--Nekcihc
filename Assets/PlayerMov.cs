using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    float x;
    float z;
    Vector3 move; 
    public float speed = 12f;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    float jumpHeight=3f;
    // Start is called before the first frame update
    public CharacterController controller;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if(isGrounded && velocity.y<0){
            velocity.y=-2f;
        }
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;
        controller.Move(move* speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y=Mathf.Sqrt(jumpHeight*-2f* gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
    }
}
