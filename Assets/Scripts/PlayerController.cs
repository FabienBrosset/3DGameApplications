using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SettingsData;

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
    public GameObject spawnPoint;
    bool m_Started;
    bool onCollision;

    public SettingsData SettingsData;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<MeshCollider>();
        anim = GetComponent<Animator>();
        m_Started = true;
        onCollision = false;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * maxSpeed;

        if (Input.GetKeyDown(SettingsData.savedData.keyboard.Jump) && grounded)
        {
            jumpRequest = true;
            anim.SetTrigger("IsJumping");
        }
        if (rb.position.y < -10f)
        {
            die();
        }
        if (!grounded && onCollision) {
            move = 0;
        }
        float fall = rb.velocity.y;
        if (fall < 0.1f && fall > -0.1f)
        {
            fall = 0;
        }
        walkingAnimation(move);
        rb.velocity = new Vector2(move, fall);
    }

    void FixedUpdate()
    {
        if (jumpRequest)
        {
            Vector3 pos = transform.position;
            float bounce = 1f;

            pos.y += 0.1f;
            Collider[] hitColliders = Physics.OverlapBox(pos, transform.localScale / 3, Quaternion.identity, mask);

            foreach (Collider col in hitColliders)
            {
                if (col.tag == "Bounce")
                    bounce = 1.5f;
            }

            rb.AddForce(Vector2.up * (jumpVelocity * bounce), ForceMode.Impulse);

            jumpRequest = false;
            grounded = false;
        }

        if (grounded && rb.velocity.y < 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            anim.SetTrigger("IsOnGround");
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
        int i = 0;
        Vector3 pos = transform.position;

        pos.y += 0.05f;
        Collider[] hitColliders = Physics.OverlapBox(pos, new Vector3(0.05f, 0.2f, 0.2f), Quaternion.identity, mask);

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].name != "ybot")
            {
                return true;
            }
            i++;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 pos = transform.position;

        pos.y += 0.05f;
        if (m_Started)
            Gizmos.DrawWireCube(pos, new Vector3(0.05f, 0.2f, 0.2f));
    }

    private void die()
    {
        transform.localPosition = spawnPoint.transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        onCollision = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollision = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NormalInk")
        {
            GameObject.Find("DrawingArea").GetComponent<DrawingManager>().AddInk(0, 10);
            other.gameObject.GetComponent<onCollectInk>().displayText();
            Destroy(other.gameObject);
        }
        if (other.tag == "BounceInk")
        {
            GameObject.Find("DrawingArea").GetComponent<DrawingManager>().AddInk(1, 10);
            other.gameObject.GetComponent<onCollectInk>().displayText();
            Destroy(other.gameObject);
        }
        if (other.tag == "BallonInk")
        {
            GameObject.Find("DrawingArea").GetComponent<DrawingManager>().AddInk(2, 10);
            other.gameObject.GetComponent<onCollectInk>().displayText();
            Destroy(other.gameObject);
        }
        if (other.tag == "FadeInk")
        {
            GameObject.Find("DrawingArea").GetComponent<DrawingManager>().AddInk(3, 10);
            other.gameObject.GetComponent<onCollectInk>().displayText();
            Destroy(other.gameObject);
        }
        if (other.tag == "Lava")
        {
            die();
        }
    }
}
