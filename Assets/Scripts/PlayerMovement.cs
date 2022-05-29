using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public AudioSource footSteps;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 move;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake() {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        // Get inputs from keyboard or controller
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        //move = (transform.right * x) + (transform.forward * z);
            
        //controller.SimpleMove(Time.deltaTime * move * speed);
        Vector3 movement = Vector3.zero;

        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && isGrounded)
        {
            if (!footSteps.isPlaying)
            {
                footSteps.Play();
            }
        }

        if (!isGrounded && footSteps.isPlaying)
        {
            footSteps.Stop();
        }
        
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            footSteps.Stop();
        }
        
        controller.Move(Time.deltaTime * speed * (transform.forward * Input.GetAxis("Vertical") +
                                          transform.right * Input.GetAxis("Horizontal")));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Gravity velocity.
        velocity.y += gravity * Time.deltaTime;
        
        // Multiply with deltaTime again for t^2
        controller.Move(velocity * Time.deltaTime);
    }
}