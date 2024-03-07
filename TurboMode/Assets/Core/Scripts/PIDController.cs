using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


[System.Serializable]
public class PIDController
{
    public float pCoeff = 0.8f;
    public float iCoeff = 0.002f;
    public float dCoeff = 0.2f;
    public float minimum = -1.0f;
    public float maximum = 1.0f;

    float integral;
    float lastProportional;

    public float Seek(float seekValue, float currentValue)
    {
        float deltaTime = Time.fixedDeltaTime;
        float proportional = seekValue - currentValue;

        float derivative = (proportional - lastProportional) / deltaTime;
        integral += proportional + deltaTime;
        lastProportional = proportional;

        //The PID formula
        float value = pCoeff * proportional + iCoeff * integral + dCoeff * derivative;
        value = Mathf.Clamp(value, minimum, maximum);

        return value;
    }
}
