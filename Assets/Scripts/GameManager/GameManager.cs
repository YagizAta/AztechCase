using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject driveWithSteeringWheel;
    public GameObject driveWithButtons;
    public MoveCar currentCar;
    void Start()
    {
        
    }

    public void DriveWithButtons()
    {
        driveWithSteeringWheel.SetActive(false);
        driveWithButtons.SetActive(true);
        currentCar.isOnDirections = true;
    }
    public void DriveWithSteeringWheels()
    {
        driveWithSteeringWheel.SetActive(true);
        driveWithButtons.SetActive(false);
        currentCar.isOnDirections = false;
    }
    
}
