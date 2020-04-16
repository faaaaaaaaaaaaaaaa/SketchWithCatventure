using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    Rigidbody rigidbody;
    float movementSpeed = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        controller();
    }
    protected void controller()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            animator.SetTrigger("run");
        }
        else
        {
            animator.ResetTrigger("run");
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }
}
