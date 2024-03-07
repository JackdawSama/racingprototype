using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float speed;
    public Transform shipModel;

    [Header("Drive Settings")]
    public float driveForce = 17f;
    public float coastingForce = 0.99f;
    public float brakeForce = 0.95f;
    public float shipRoll = 30f;

    [Header("Hover  Settings")]
    public float hoverHeight = 2.0f;
    public float maxAirDist = 5.0f;
    public float hoverForce = 300f;
    public Transform[] raycasters = new Transform[4];
    public LayerMask groundLayer;
    public PIDController PID;

    [Header("Physics Settings")]
    public float maxVelocity = 100.0f;
    public float hoverGravity = 20.0f;
    public float fallGravity =  80.0f;
    

    //Private Data Variables
    Rigidbody rb;
    PlayerInput input;
    float drag;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        drag = driveForce/maxVelocity;
    }

    void Update()
    {
        RayCastDebugInfo();
    }

    void FixedUpdate()
    {
        for(int i = 0; i < raycasters.Length; i++)
        {
            HandleHover(raycasters[i]);
        }
    }

    void HandleHover(Transform raycaster)
    {
        Vector3 groundNormal;

        Ray ray = new Ray(raycaster.position, -raycaster.up);

        RaycastHit hitInfo;

        isGrounded = Physics.Raycast(ray, out hitInfo, maxAirDist, groundLayer);

        if(isGrounded)
        {
            float height = hitInfo.distance;
            groundNormal = hitInfo.normal.normalized;

            float forceCorrectedRatio = PID.Seek(hoverHeight, height);

            Vector3 force = groundNormal * hoverForce * forceCorrectedRatio;

            Vector3 gravity = -groundNormal * hoverGravity * height;

            rb.AddForce(force, ForceMode.Acceleration);
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
        else
        {
            groundNormal = Vector3.up;

            Vector3 gravity = -groundNormal * fallGravity;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }

        Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
        Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);

        rb.MoveRotation(Quaternion.Lerp(rb.rotation, rotation, Time.deltaTime * 10.0f));

        float angle = shipRoll * -input.hInput;

        Quaternion shipRotation = transform.rotation * Quaternion.Euler(0, 0, angle);
        shipModel.rotation = Quaternion.Lerp(shipModel.rotation, shipRotation, Time.deltaTime * 10.0f);
    }

    void HandleDrive()
    {

    }

    void RayCastDebugInfo()
    {
        for(int i = 0; i < raycasters.Length; i++)
        {
            Debug.DrawRay(raycasters[i].position, -Vector3.up * maxAirDist, Color.red);
        }
    }
}
