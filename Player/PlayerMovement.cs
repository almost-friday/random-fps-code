using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the player's movement

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 6;
    [SerializeField] private float Acceleration = 400;
    [SerializeField] private float JumpSpeed = 6;
    [SerializeField] private LayerMask mask;
    [SerializeField] private CapsuleCollider col = null;
    private MouseLook mouseLook;
    private Camera cam;

    private Rigidbody rigid;
    private bool isMoving;
    private bool isGrounded;
    private float drag;
    private bool previouslyGrounded;
    private bool isJumping = false;
    private bool isRocketJumping = false;
    private bool checkGrounded = true;

    RaycastHit hit;
    Vector3 InputVector = Vector3.zero;
    Vector2 RocketJumpVector = Vector2.zero;

    Vector3 PlaneVector;
    float PlaneSpeed;

    void Awake(){
        cam = GetComponentInChildren<Camera>();
        mouseLook = cam.GetComponent<MouseLook>();
        rigid = GetComponent<Rigidbody>();
        drag = rigid.drag;
    }

    void Update(){
        InputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlaneVector = GetSpeed();
    }

    private void LateUpdate()
    {
        mouseLook.RotateCamera(transform);
    }

    void FixedUpdate() {
        // Raycasts down to see if any ground is detected
        previouslyGrounded = isGrounded;

        if (checkGrounded)
        {
            // Sends a spherecast down to detect if effectively touching the ground
            isGrounded = Physics.SphereCast(transform.position + col.center, col.radius, Vector3.down, out hit, col.height/2 + 0.1f, mask);

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                if (Vector3.Angle(Vector3.up, hit.normal) > 10)
                {
                    if (isMoving)
                    {
                        if (!isJumping && !isGrounded) rigid.velocity += Physics.gravity;
                        rigid.drag = drag;
                    }
                    else if (isGrounded && !isJumping && !isRocketJumping) rigid.drag = 75;
                }
            }
            else if (!isGrounded && (rigid.drag == 75)) rigid.drag = drag;
        }

        isMoving = InputVector.magnitude > .01f;
        if (isMoving) PlaneSpeed = PlaneVector.magnitude;
        else PlaneSpeed = 0;

        if (isGrounded) {
            isRocketJumping = false;
            if (!previouslyGrounded) ResetJump();
            if (Input.GetButton("Jump") && !isJumping) Jump();
        }

        // Checks if any horizantal/vertical input is detected
        if (!isRocketJumping)
        {
            if (isMoving) Move(InputVector);
            else if (!isJumping) Decelerate();
        } else
        {
            if (isMoving)
            {
                MidairMove(InputVector);
            }
        }
    }

    IEnumerator throttleJump()
    {
        isGrounded = false;
        checkGrounded = false;
        yield return new WaitForSeconds(0.3f);
        checkGrounded = true;
    }
    void Move(Vector3 input) {
        rigid.velocity += (transform.right * input.x * Acceleration * Time.deltaTime);
        rigid.velocity += (transform.forward * input.z * Acceleration * Time.deltaTime);

        SpeedCheck(MaxSpeed);
    }
    float speed = 0;
    void MidairMove(Vector3 input)
    {
        PlaneVector = GetSpeed();
        if (PlaneSpeed < speed)
        {
            if (speed > MaxSpeed) {
                speed = PlaneSpeed;
                Debug.Log(speed);
            }
            else speed = MaxSpeed;
        }
        SpeedCheck(speed);

        if (input.x > 0 ? rigid.velocity.x < speed + Mathf.Abs(RocketJumpVector.x) : rigid.velocity.x > -speed - Mathf.Abs(RocketJumpVector.x))
        {
            float targetX = input.x * (Acceleration / 4) * Time.deltaTime;
            rigid.velocity += transform.right * targetX;
        }

        if (input.z > 0 ? rigid.velocity.z < speed + Mathf.Abs(RocketJumpVector.x) : rigid.velocity.z > -speed - Mathf.Abs(RocketJumpVector.x))
        {
            float targetY = input.z * (Acceleration / 4) * Time.deltaTime;
            rigid.velocity += transform.forward * targetY;
        }
        //if (input.x < 0 ? rigid.velocity.x > MaxSpeed + Mathf.Abs(RocketJumpVector.x) : rigid.velocity.x < -MaxSpeed - Mathf.Abs(RocketJumpVector.x)) SpeedCheck();
    }
    void SpeedCheck(float maxSpeed)
    {
        PlaneVector = GetSpeed();
        Vector3 MaxPlaneSpeed = PlaneVector.normalized * maxSpeed;
        if (PlaneSpeed > maxSpeed)
        {
            rigid.velocity = new Vector3(MaxPlaneSpeed.x, rigid.velocity.y, MaxPlaneSpeed.z);
        }
        else
        {
            rigid.velocity = new Vector3(PlaneVector.x, rigid.velocity.y, PlaneVector.z);
        }
    }

    void Decelerate (){
        rigid.velocity -= new Vector3(rigid.velocity.x * 10f * Time.deltaTime, 0, rigid.velocity.z * 10f * Time.deltaTime);
    }
    void Jump(){
        isGrounded = false;
        rigid.drag = drag;
        isJumping = true;
        rigid.velocity = new Vector3(rigid.velocity.x, JumpSpeed, rigid.velocity.z);
    }
    void ResetJump(){
        RocketJumpVector = Vector2.zero;
        isJumping = false;

    }
    public void RocketJump(Vector3 orgin, float explosionForce, float explosionRadius)
    {
        isGrounded = false;
        isJumping = true;
        rigid.drag = drag;
        StartCoroutine(throttleJump());
        isRocketJumping = true;
        Vector3 dir = Vector3.Normalize(cam.transform.position - orgin);
        float force = explosionForce * Mathf.Clamp01(1 - (Vector3.Distance(cam.transform.position, orgin) / explosionRadius));
        rigid.velocity = new Vector3(rigid.velocity.x + (dir.x * force), rigid.velocity.y + (dir.y * force), rigid.velocity.z + (dir.z * force)); // Takes the place of AddForce
        RocketJumpVector = new Vector2(rigid.velocity.x, rigid.velocity.z);
        speed = RocketJumpVector.magnitude > MaxSpeed ? RocketJumpVector.magnitude : MaxSpeed;
    }

    Vector3 GetSpeed()
    {
        
        return new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
    }
}
