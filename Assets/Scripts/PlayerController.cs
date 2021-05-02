using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshCollider bc;
    private Animator anim;
    private bool jumpRequest = true;
    private bool grounded = true;

    public float maxSpeed;
    public float jumpVelocity;
    public LayerMask mask;
    public int ink;
    public GameObject spawnPoint;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<MeshCollider>();
        anim = GetComponent<Animator>();
        ink = 0;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * maxSpeed;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
            anim.SetTrigger("IsJumping");
        }
        if (rb.position.y < 0)
        {
            die();
        }
        walkingAnimation(move);
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

    private void walkingAnimation(float move)
    {
        grounded = IsGrounded();
        if (move != 0f)
            anim.SetTrigger("IsWalking");
        else
            anim.SetTrigger("IsWaiting");
        if (move > 0)
            this.gameObject.transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);
        else if (move < 0)
            this.gameObject.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.up);
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask))
        {
            if (hit.distance < 0.1f)
            {
                if (!grounded)
                    anim.SetTrigger("IsOnGround");
                return true;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }

        return false;
    }

    private void die()
    {
        transform.localPosition = spawnPoint.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ink")
        {
            Debug.Log("+100 ink");
            ink += 100;
            Destroy(other.gameObject);
        }
        if (other.tag == "Lava")
        {
            die();
        }
    }
}