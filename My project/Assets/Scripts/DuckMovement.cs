using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public CharacterController controller;

    public Animator animator;

    public float speed;
    public float gravity;

    public float jumpHeight;

    public Transform groundCheck;
    float groundDistance = 0.4f;
    public LayerMask groundMask;

    float OGstepOffSet;

    Vector3 velocity;
    bool isGrounded;

    public float leftPosition;
    public float middlePosition;
    public float rightPosition;

    string move_CMD = "";

    public float shiftSpeed;

    string currentPath;

    void Start()
    {
        OGstepOffSet = controller.stepOffset;
    }
    void Update()
    {
        //Path Switching (Inputting)
        if (Input.GetButtonDown("Fire1"))
        {
            //Shift("Left");
            if (currentPath == "Right")
            {
                if (transform.position.x > middlePosition)
                {
                    move_CMD = "RM";
                }
            }
            if (currentPath == "Middle")
            {
                if (transform.position.x > leftPosition)
                {
                    move_CMD = "ML";
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //Shift("Right");
            if (currentPath == "Left")
            {
                if (transform.position.x < middlePosition)
                {
                    move_CMD = "LM";
                }
            }
            if (currentPath == "Middle")
            {
                if (transform.position.x < rightPosition)
                {
                    move_CMD = "MR";
                }
            }
        }
        //Path Switching (Checking)
        if (transform.position.x > rightPosition - 0.1f && transform.position.x < rightPosition + 0.1f)
        {
            currentPath = "Right";
        }
        else if(transform.position.x > middlePosition - 0.1f && transform.position.x < middlePosition + 0.1f)
        {
            currentPath = "Middle";
        }
        else if(transform.position.x > leftPosition - 0.1f && transform.position.x < leftPosition + 0.1f)
        {
            currentPath = "Left";
        }

        //Path Switching (Moving)
        if(currentPath != "Middle" && move_CMD == "RM")
        {
            Vector3 _move = new Vector3(-1, 0, 0);
            controller.Move(_move * Time.deltaTime * shiftSpeed);
        }
        if (currentPath != "Left" && move_CMD == "ML")
        {
            Vector3 _move = new Vector3(-1, 0, 0);
            controller.Move(_move * Time.deltaTime * shiftSpeed);
        }
        if (currentPath != "Middle" && move_CMD == "LM")
        {
            Vector3 _move = new Vector3(1, 0, 0);
            controller.Move(_move * Time.deltaTime * shiftSpeed);
        }
        if (currentPath != "Right" && move_CMD == "MR")
        {
            Vector3 _move = new Vector3(1, 0, 0);
            controller.Move(_move * Time.deltaTime * shiftSpeed);
        }

        //Running
        Vector3 move = transform.forward * speed;
        controller.Move(move * speed * Time.deltaTime);

        //Jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            controller.stepOffset = OGstepOffSet;
            animator.SetFloat("Run", 1);
            animator.SetFloat("Jump", 0);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(Jumping());
            animator.SetFloat("Run", 0);
            animator.SetFloat("Jump", 1);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.4f);
        controller.stepOffset = 2;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    
}