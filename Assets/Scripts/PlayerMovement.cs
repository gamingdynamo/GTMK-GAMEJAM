using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float movespeed;
    [SerializeField] private Rigidbody rigidbody_e;
    private Vector2 turn;
    public float sensitivity = 500f;
    private Vector2 movement;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turn += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        rb.velocity = transform.forward * movement.y * movespeed * Time.deltaTime + transform.right * movement.x * movespeed * Time.deltaTime;
        rb.velocity=new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    private void OnMove(InputValue val)
    {
        movement = val.Get<Vector2>();
    }
    private void OnPull()
    {
        
    }
}
