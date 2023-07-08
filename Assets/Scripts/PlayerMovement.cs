using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 10.0f;

    public Vector2 input;
    Camera mainCamera;
    Rigidbody rb;

    Vector3 targetDirection;
    private Quaternion freeRotation;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateMovement();
        UpdateAnimation();
    }

    private void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        input = new Vector2(x, y);

        var m_CamForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        var dirRight = Vector3.Scale(mainCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        var m_Move = (input.y * m_CamForward) + (input.x * dirRight);

        Vector3 force = (m_Move * moveSpeed);
        force = Vector3.ProjectOnPlane(force, Vector3.up) * input.magnitude;

        rb.velocity = force;
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void LateUpdate()
    {
        FaceDirection();
    }

    void FaceDirection()
    {
        var forward = mainCamera.transform.forward;
        var right = mainCamera.transform.right;

        targetDirection = input.x * right + input.y * forward;
        
        if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * Time.deltaTime);
        }
    }
}
