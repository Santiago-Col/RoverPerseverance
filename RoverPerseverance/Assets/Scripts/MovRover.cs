using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axle
{
    Front,
    Back
}

public struct Wheel
{
    public GameObject model;
    public WheelCollider wheelCollider;
    public Axle axle;
}

[RequireComponent(typeof(Rigidbody))]
public class MovRover : MonoBehaviour
{
    public float maxAcceleration = 30.0f;    
    private float turnSensitive = 1.0f;    
    private float maxAngle = 45.0f;
    private float brakeForce = 5000.0f;

    private float inputX;
    private float inputY;
    private bool isBreaking;

    private Rigidbody _rb;

    [Header("Input")]
    public List<AxleInfo> axleInfos = new List<AxleInfo>();

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInputs();
        Move();
    }

    private void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void Move()
    {
        foreach (AxleInfo info in axleInfos)
        {
            brakeForce = isBreaking ? 5000f : 0f;
            if (info.isBack)
            {
                info.rigtWheel.motorTorque = inputY * maxAcceleration * 200 * Time.deltaTime;
                info.leftWheel.motorTorque = inputY * maxAcceleration * 200 * Time.deltaTime;
                info.rigtWheel.brakeTorque = brakeForce;
                info.leftWheel.brakeTorque = brakeForce;
            }
            if (info.isFront)
            {
                var _steerAngle = inputX * turnSensitive * maxAngle;
                info.rigtWheel.steerAngle = Mathf.Lerp(info.rigtWheel.steerAngle, _steerAngle, 1.0f);
                info.leftWheel.steerAngle = Mathf.Lerp(info.leftWheel.steerAngle, _steerAngle, 1.0f);
                info.rigtWheel.brakeTorque = brakeForce;
                info.leftWheel.brakeTorque = brakeForce;
            }

            AnimateWheels(info.rigtWheel, info.visualRightWheel);
            AnimateWheels(info.leftWheel, info.visualLeftWheel);
        }
    }

    private void AnimateWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Quaternion _rot;
        Vector3 _pos;

        Vector3 rotate = Vector3.zero;

        wheelCollider.GetWorldPose(out _pos, out _rot);
        wheelTransform.transform.rotation = _rot;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider rigtWheel;
    public WheelCollider leftWheel;

    public Transform visualRightWheel;
    public Transform visualLeftWheel;

    public bool isBack;
    public bool isFront;
}
