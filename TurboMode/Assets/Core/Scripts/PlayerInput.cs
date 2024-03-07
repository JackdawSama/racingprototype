using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float vInput;
    [HideInInspector] public float hInput;
    [HideInInspector] public KeyCode brakeInput = KeyCode.Space;
    [HideInInspector] public KeyCode boostInput = KeyCode.LeftShift;

    [HideInInspector] public bool isBraking = false;

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(brakeInput);
    }
}
