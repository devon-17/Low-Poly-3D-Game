using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerPlayerController : MonoBehaviour
{
    [Header("--- PLAYER MOVEMENT ---")]
    [Range(1, 10)] public float speed;
    [Range(1, 10)] public float speedAfterCollision;
    [Range(1, 10)] public float jumpSpeed;
    float moveVelocity;
    public GameObject moveObj;

    // other
    GameObject player;
    [HideInInspector] public bool isGrounded = true;
    Rigidbody rb;
    [HideInInspector] public bool canMove = false;


    // movement
    [HideInInspector] public bool isMovingLeft;
    [HideInInspector] public bool isMovingRight;


    [Header("--- LIVES ---")]
    public int lives = 3;
    public Text livesText;

    [Header("--- RESPAWN ---")]
    public Transform respawnPoint;

    [HideInInspector] public Distance distance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player GFX");
        distance = FindObjectOfType<Distance>();
        livesText.text = "Lives: " + lives;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Jump();
            Movement();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Check if Grounded
        isGrounded = true;
    }


    public void Jump()
    {
        bool isJumping = Input.GetButtonDown("Jump");
        //Jumping
        if (isJumping && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isGrounded = false;
        }
    }

    public void Movement()
    {
        moveVelocity = 0;

        isMovingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        isMovingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        //Left Right Movement
        if (isMovingLeft)
        {
            moveVelocity = -speed;
            transform.eulerAngles = new Vector3(0, -90, 0); // Flipped
        }
        if (isMovingRight)
        {
            transform.eulerAngles = new Vector3(0, 90, 0); // Flipped
            moveVelocity = speed;
        }

        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
    }

    public void ResetCamera()
    {
        moveObj.transform.position = this.transform.position;
    }

    public void StopCamera()
    {
        moveObj.transform.position = this.transform.position;
        moveObj.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Moving Collider")
        {
            StartCoroutine(RightWall());
        }
    }

    public IEnumerator RightWall()
    {
        speed = 0;
        yield return new WaitForSeconds(1f);
        speed = speedAfterCollision;
    }
}
