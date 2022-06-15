using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    public AudioSource jumpSound;
    public AudioSource walkSound;

    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpSound.Play();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        float sensitivity = PlayerPrefs.GetFloat("masterSen");

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            walkSound.Play();
        else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && walkSound.isPlaying)
            walkSound.Stop();
    }

    
}