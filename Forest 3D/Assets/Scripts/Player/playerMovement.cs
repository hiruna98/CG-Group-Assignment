using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerMovement : NetworkBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = 10f;

    Vector3 velocity;
    public float jumpHeight = 3f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(this);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;


        if(Input.GetButtonDown("Jump")){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        controller.Move(velocity * Time.deltaTime);

    }
}
