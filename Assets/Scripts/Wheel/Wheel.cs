using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool powered;
    public float maxAngle = 90f;
    public float offset;
    public float breakTorque = 200;
    
    private float turnAngle;
    public WheelCollider wheelCollider;
    private Transform wheelMesh;

    private Quaternion firstRot;
    private void Start()
    {
        wheelMesh = transform;
        firstRot = wheelMesh.transform.localRotation;
    }
    public void Steer(float steerInput)
    {
        turnAngle = steerInput * maxAngle + offset;
        wheelCollider.steerAngle = turnAngle;
    }

    public void Accelerate(float powerInput)
    {
        if (powerInput!=0)
        {
            wheelCollider.brakeTorque = 0;
        }
        if(powered) wheelCollider.motorTorque = powerInput;
        else wheelCollider.brakeTorque = 0;
    }

    public void Break()
    {
        wheelCollider.brakeTorque = breakTorque;
    }
    public void UpdatePosition()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = rotation;
    }
}
