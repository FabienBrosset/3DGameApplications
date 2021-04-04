using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshCollider bc;
    private bool jumpRequest = true;
    private bool grounded = true;

    public float maxSpeed;
    public float jumpVelocity;
    public float groundedSkin = 0.1f;
    public LayerMask mask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<MeshCollider>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * maxSpeed;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
        }

        grounded = IsGrounded();

        rb.velocity = new Vector2(move, rb.velocity.y);
    }

    void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode.Impulse);

            jumpRequest = false;
            grounded = false;
        }
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask))
        {
            if (hit.distance < 0.1f)
            {
                return true;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }

        return false;
    }
}
