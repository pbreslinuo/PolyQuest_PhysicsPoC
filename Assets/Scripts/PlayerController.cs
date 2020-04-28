using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameLevel gameLevel;

    private Rigidbody rb;
    private bool space;        // checks to see if spacebar is hit
    private bool down;         // checks to see if "s" is hit
    private bool onGround;     // detects being on the ground
    private bool dblJump;      // keeps track of double jump usage
    private float startTime;
    private float cooldown;    // cd for edge rotations
    private float y_maxVelocity;

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();
        cooldown = 0.1f;       // long enough where cooldown is longer than one frame -- might be an issue at 10fps and at cube corners
        y_maxVelocity = 30.0f;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3 (moveHorizontal - 5*rb.velocity.x/speed, 0.0f, 0.0f); // artifical drag only in x direction
        if (onGround == true)
        { // lock in velocities when jumping or falling
            rb.AddForce(movement * speed);
        }
          // restrict falling speed to 30
        if (rb.velocity.y < -y_maxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -y_maxVelocity, rb.velocity.z);
        }
    }

    private void Update()
    {
        space = Input.GetKeyDown("space");
        if (space & (onGround | dblJump))
        {
            Vector3 jump = new Vector3(0.0f, 7.0f - rb.velocity.y, 0.0f);
            rb.AddForce(jump, ForceMode.VelocityChange);
            if (onGround == false)
            {
                dblJump = true;    // true: infinite jumps. false: double jump only
            }
        }
        down = Input.GetKeyDown("s");
        if (down & onGround)       // to do: make intentionally falling through planes more seamless
        {
            rb.transform.position += new Vector3(0.0f, -0.5f, 0.0f);
        }
    }

    void OnCollisionExit(Collision other)
    { // detects when the ball leaves the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void OnCollisionStay(Collision other)
    { // maintain these values when ball stays on ground
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dblJump = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Edge") && Time.time - startTime > cooldown) // use && other.attachedRigidbody.IsSleeping()) instead for animated version
        { // if the player hits an edge
            startTime = Time.time;
            gameLevel.RotateLevel(other.gameObject);
        }
    }
}