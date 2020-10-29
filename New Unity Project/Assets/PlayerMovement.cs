using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MovementsObj))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    public MovementsObj movement;
    

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        movement = gameObject.GetComponent<MovementsObj>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, movement.verticalSpeed);
        controller.Move(move * Time.deltaTime * movement.horizontalSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(movement.jumpHeight * -3.0f * movement.gravityValue);
        }

        
    }

    private void FixedUpdate()
    {
        playerVelocity.y += movement.gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}