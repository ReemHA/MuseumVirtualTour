using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRotTiltMode : MonoBehaviour
{
    private bool switchRotTMode;
    public GameObject CamObj;
    public GameObject RightJoystick;
    public GameObject LeftJoystick;
    public Image SwtchImg;
    public Sprite JoyOnSp;
    public Sprite TiltOnSp;

    public void SwitchBtn()
    {
        if (!switchRotTMode)
        {
            RightJoystick.SetActive(false);
            LeftJoystick.SetActive(false);
            CamObj.GetComponent<GyroController>().enabled = true;
            SwtchImg.sprite = TiltOnSp;
            switchRotTMode = true;
        }
        else
        {
            RightJoystick.SetActive(true);
            LeftJoystick.SetActive(true);
            CamObj.GetComponent<GyroController>().enabled = false;
            SwtchImg.sprite = JoyOnSp;
            switchRotTMode = false;
        }
    }
}