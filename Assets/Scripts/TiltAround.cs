using UnityEngine;
using System.Collections;

public class TiltAround : MonoBehaviour
{
    private Quaternion localRotation; // 
    public float speed; // ajustable speed from Inspector in Unity editor

    // Use this for initialization
    void Start()
    {
        // copy the rotation of the object itself into a buffer
        localRotation = transform.localRotation;
    }

    Matrix4x4 calibrationMatrix;

    Vector3 wantedDeadZone = Vector3.zero;

    //Method for calibration 
    void calibrateAccelerometer()
    {
        wantedDeadZone = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), wantedDeadZone);
        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));
        //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;

    }

    //Method to get the calibrated input 
    Vector3 getAccelerometer(Vector3 accelerator)
    {
        Vector3 accel = this.calibrationMatrix.MultiplyVector(accelerator);
        return accel;
    }

    //Finally how you get the accelerometer input
    Vector3 _InputDir;

    Vector3 Temp;
    //you also want to calibrate the device on start so regardless of how user is holding it the initial position will be treated as 0 Input. 
    void OnEnable()
    {
        Temp = transform.eulerAngles;
        calibrateAccelerometer();
    }

    void OnDisable()
    {
        transform.eulerAngles = Temp;
    }


    void Update() // runs 60 fps or so
    {
        _InputDir = getAccelerometer(Input.acceleration);
        //then in your code you use _InputDir instead of Input.acceleration for example 
        //transform.Translate(_InputDir.x, 0, -_InputDir.z);

        //// find speed based on delta
        float curSpeed = Time.deltaTime * speed * 5;
        //// first update the current rotation angles with input from acceleration axis
        // localRotation.y += Input.acceleration.x * curSpeed;
        //localRotation.x += Input.acceleration.y * curSpeed;
        //localRotation.z = 0;
        ////localRotation.y = transform.localRotation.y;

        ////if (localRotation.x < 20 && localRotation.x > -20)
        ////{
        //    // then rotate this object accordingly to the new angle
        //    transform.localRotation = localRotation;
        ////}

        float pitch = _InputDir.y * 1f * curSpeed;
        if ((pitch > 0 && transform.rotation.x > -20) || (pitch < 0 && transform.rotation.x < 20))
            transform.Rotate(pitch * Vector3.right, Space.Self);


        float yaw = _InputDir.x * curSpeed;
        transform.Rotate(yaw * Vector3.up, Space.Self);

        // transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);

    }

}