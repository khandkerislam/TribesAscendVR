using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float slipSpeed;
    public float speedAccelerate = 20;
    public float speedDeccelerate = 20;

    float movementSpeed;
    float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Jetpack")]
    public float jetPack = 5f;

    public float currentBoost;

    public float jetPackMeter;
    public float energyCost;
    public float energyRegen = .025f;
    public float regenTime = 3;

    public float jetPackMeterMax = 5;

    [Header("Skiing")]
    public float normalFriction = 1;
    public float slipFriction = 0;

    [Header("Drag")]
    public float groundDrag = 5f;
    public float airDrag = 0f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    public bool isGrounded;
    float groundDistance;
    RaycastHit slopeHit;
    RaycastHit Hit;
    LayerMask groundMask;

    Rigidbody rb;
    Collider playerMaterial;


    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerMaterial = GetComponent<Collider>();

        groundDistance = 0;
    }

    private void Update()
    {
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);
        //isGrounded = Physics.SphereCast(transform.position, .23f, Vector3.down, out Hit, groundDistance, groundMask);
        //isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance + 0.1f);

        MyInput();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Hit");
            //playerMaterial.material.dynamicFriction = 0;
            //playerMaterial.material.staticFriction = 0;
            movementSpeed = Mathf.Lerp(movementSpeed, slipSpeed, Time.deltaTime * speedAccelerate);
        }
        else
        {
            //playerMaterial.material.dynamicFriction = 1;
            //playerMaterial.material.staticFriction = 1;
            movementSpeed = Mathf.Lerp(movementSpeed, moveSpeed, Time.deltaTime * speedDeccelerate);
        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        if (!isGrounded || Input.GetKey(KeyCode.Space))
        {
            rb.drag = airDrag;
        }
        else
        {
            rb.drag = groundDrag;
        }
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void ControlDrag()
    {

    }

    void Jump()
    {
        rb.AddForce(transform.up * jetPack, ForceMode.Impulse);
    }
    public float Lerp(float valueToLerp, float startValue, float endValue, float lerpDuration)
    {
        float timeElapsed = 0;

        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            valueToLerp = endValue;
        }
        return valueToLerp;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        JetPack();

    }

    void JetPack()
    {
        if (Input.GetMouseButton(0) && jetPackMeter > 0)
        {
            currentBoost = jetPackMeter;
            currentBoost -= jetPackMeterMax / 100;
            //jetPackMeter = Lerp(jetPackMeter,5,0,3);
            //Debug.Log(jetPackMeter);
            jetPackMeter = currentBoost;
            Jump();
        }
        else if (!Input.GetMouseButton(0) && jetPackMeter < jetPackMeterMax)
        {
            //jetPackMeter = Mathf.Lerp(jetPackMeter, jetPackMeterMax, energyRegen);
            //timeElapsed += Time.deltaTime
            currentBoost = jetPackMeter;
            currentBoost += jetPackMeterMax / 150;
            jetPackMeter = currentBoost;
        }
    }
}
