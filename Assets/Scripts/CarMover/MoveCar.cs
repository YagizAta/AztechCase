using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    public Transform steeringWheel;
    public Transform gravityTarget;
    public DynamicJoystick DynamicJoystick;
    public float maxRot;
    public float power = 15000f;

    public float gravity = 9.81f;

    public bool isOnDirections = false;
    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    private float horInput;
    private float verInput;
    private float steerAngle;

    public Wheel[] wheels;

    Rigidbody rb;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void Update() 
    {
        if (Input.GetButton("Fire1"))
        {
            ProcessInput();
            GetTheDifferance();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            TurnZeroSteeringWheel();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            ProcessForces();
            ProcessGravity();
        }
    }

    private void GetTheDifferance()
    {
        Vector3 diff = transform.position - gravityTarget.position;
        if (autoOrient)
        {
            AutoOrient(-diff);
        }
    }

    public void TurnZeroSteeringWheel()
    {
        float time = 0.8f;
       
       foreach(Wheel w in wheels)
       {
           w.Break();
       }
       
       if (!isOnDirections)
       {
           horInput.TweenFloatAndSet(0, time, SetHorizontal, null);
       }
       else
       {
           ProcessForces();
       }

    }

    private void SetHorizontal(float value)
    {
        horInput = value;
        horInput = Mathf.Clamp(horInput, -1, 1);
        ProcessForces();
    }

    public void SetMovement(float value)
    {
        verInput = value;
        AllNeededThingsForMovement();
    }

    public void SetDirection(float value)
    {
        horInput.TweenFloatAndSet(horInput + value, 0.2f, SetHorizontal, null);
    }

    private void AllNeededThingsForMovement()
    {
        ProcessInput();
        Vector3 diff = transform.position - gravityTarget.position;
        if(autoOrient) { AutoOrient(-diff); }
        ProcessForces();
        ProcessGravity();
    }
    void ProcessInput()
    {
        if (!isOnDirections)
        {
            horInput = DynamicJoystick.Horizontal;
        }
      
    }

    void ProcessForces()
    {
        
        steeringWheel.transform.DOKill();
       
        Quaternion currentRotation = steeringWheel.localRotation;

        Vector3 vec = steeringWheel.localEulerAngles;
        vec.z = horInput * maxRot;
        
        steeringWheel.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x,currentRotation.eulerAngles.y,vec.z);
        
        foreach(Wheel w in wheels)
        {
            w.Steer(horInput);
            
            w.Accelerate(verInput * power);
            w.UpdatePosition();
        }
    }

    void ProcessGravity()
    {
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));
    }

    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
