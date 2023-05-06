using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarMovementUI : MonoBehaviour
{
    public static CarMovementUI instance;
    public TextMeshProUGUI gasText;
    public MoveCar currentCar;
    
    private bool isForward = true;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }




    public void Gas()
    {
        if (isForward)
        {
            gasText.text = "BACK";
            currentCar.SetMovement(1);
        }
        else
        {
            gasText.text = "FORWARD";
            currentCar.SetMovement(-1);

        }
        isForward = !isForward;
       
    }

    public void Break()
    {
        currentCar.TurnZeroSteeringWheel();
        currentCar.SetMovement(0);
    }

    public void DirectionButtons(float value)
    {
        currentCar.SetDirection(value);
    }

   
    
}
