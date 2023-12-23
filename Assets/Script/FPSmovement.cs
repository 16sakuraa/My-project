using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSmovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource JumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input in local space
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; // Ignore the vertical component

        // Transform input to world space based on camera forward direction
        Vector3 movement = (verticalInput * cameraForward + horizontalInput * Camera.main.transform.right).normalized;

        // Set player velocity
        rb.velocity = new Vector3(movement.x * movementSpeed, rb.velocity.y, movement.z * movementSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        JumpSound.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
}