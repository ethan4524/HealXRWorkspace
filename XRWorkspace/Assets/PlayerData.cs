using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VelUtils;

public class PlayerData : MonoBehaviour
{
    DontDestroyOnLoad m_DontDestroyOnLoad;

    public bool isLogging = false;
    public GameObject leftHand;
    public GameObject rightHand;

    string userNamePlaceholder = "Ethan";
    public void StartTest()
    {
        isLogging = true;
    }
    public void EndTest()
    {
        isLogging=false;
    }

    private void Start()
    {
        VelUtils.Logger.SetHeaders("PlayerData_" + userNamePlaceholder, 
            "LeftHandPosition_X", "LeftHandPosition_Y", "LeftHandPosition_Z",
            "LeftHandRotation_X", "LeftHandRotation_Y", "LeftHandRotation_Z",
            "RightHandPosition_X", "RightHandPosition_Y", "RightHandPosition_Z", 
            "RightHandRotation_X", "RightHandRotation_Y", "RightHandRotation_Z");
    }

    private void Update()
    {
        if (isLogging)
        {
            //Debug.Log(GetCurrentDateTimeAsString());
            VelUtils.Logger.LogRow("PlayerData_" + GetCurrentDateTimeAsString(), 
                new StringList(new List<dynamic> {leftHand.transform.position, leftHand.transform.rotation, rightHand.transform.position,rightHand.transform.rotation}).List);
        }
    }

    public string GetCurrentDateTimeAsString()
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;

        // Convert the current date and time to a string using a specific format
        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH");

        return formattedDateTime;
    }



}
