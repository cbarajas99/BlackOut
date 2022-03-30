//http://docs.unity3d.com/Manual/WheelColliderTutorial.html

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    [Header("Settings")]
    public bool motor;
    public bool steering;
    public bool hasBrakes;

    [Header("References")]
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
}

public class CarS : MonoBehaviour
{

    [Header("Info")]
    public float currentSpeed;
    public Vector3 currentVelocity;

    [Header("Settings")]
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque = 400f;
    public float maxSteeringAngle = 30f;
    public float currentBrakeTorque = 0f;

    // COMPONENTS
    private Rigidbody rb;

    //============================================
    // FUNCTIONS (UNITY)
    //============================================

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // BRAKE
        if (Input.GetKey(KeyCode.B))
        {
            currentBrakeTorque = 10000f;
        }
        else
        {
            currentBrakeTorque = 0f;
        }

        // JUMP
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(rb.centerOfMass + new Vector3(0f, 10000f, 0f), ForceMode.Impulse);
        }

        // BOOST
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(transform.forward * 1000f, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // SPEED
        currentVelocity = rb.velocity;
        currentSpeed = rb.velocity.magnitude;

        // INPUT
        //float motor = maxMotorTorque * Input.GetAxis("Accelerator");
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        
        

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            if (axleInfo.hasBrakes)
            {
                axleInfo.leftWheel.brakeTorque = currentBrakeTorque;
                axleInfo.rightWheel.brakeTorque = currentBrakeTorque;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    //============================================
    // FUNCTIONS (CUSTOM)
    //============================================

    // FINDS THE VISUAL WHEEL, CORRECTLY APPLIES THE TRANSFORM
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        // GET WHEEL
        Transform visualWheel = collider.transform.GetChild(0);

        // GET POS/ROT
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        // APPLY POS/ROT
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

}