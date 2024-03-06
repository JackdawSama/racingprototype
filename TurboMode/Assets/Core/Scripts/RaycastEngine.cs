using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEngine : MonoBehaviour
{
    public Transform[] raycasters = new Transform[4];
    public float raycastDistance = 1.0f;
    public LayerMask groundLayer;
    public Rigidbody rb;

    public float antiGravForce = 9.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RayCastDebugInfo();
    }

    void FixedUpdate()
    {
        for(int i = 0; i < raycasters.Length; i++)
        {
            ApplyHover(raycasters[i]);
        }
    }

    void ApplyHover(Transform raycaster)
    {
        RaycastHit hit;

        if(Physics.Raycast(raycaster.position, -Vector3.up, out hit, raycastDistance, groundLayer))
        {

            rb.AddForceAtPosition(Vector3.up * antiGravForce * rb.mass , raycaster.position);
        }
    }

    void HoverForceCorrection()
    {
        
    }

    void RayCastDebugInfo()
    {
        for(int i = 0; i < raycasters.Length; i++)
        {
            Debug.DrawRay(raycasters[i].position, -Vector3.up * raycastDistance, Color.red);
        }
    }
}
