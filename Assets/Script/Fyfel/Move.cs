using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Move : MonoBehaviour
{

	private float m_horizontalInput;
	private float m_verticalInput;

	public bool m_break;
	private float m_steeringAngle;

	public WheelCollider fl, fr;
	public WheelCollider rl, rr;
	public Transform flT, frT;
	public Transform rlT, rrT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public float breakSpeed = 50;
	public Transform centerOfMass;
	private float mass;
    public float SpeedLim = 100;
    private Rigidbody rb;
    public GameObject Player;

	public Text speed;

	void Start()
    {
    GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
	mass = GetComponent<Rigidbody>().mass;
	
    rb = GetComponent<Rigidbody>();
    }

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		Brake();
	}
    public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
		m_break = Input.GetButton("Jump");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		fl.steerAngle = m_steeringAngle;
		fr.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		fl.motorTorque = m_verticalInput * motorForce;
		fr.motorTorque = m_verticalInput * motorForce;
		rl.motorTorque = m_verticalInput * motorForce;
		rr.motorTorque = m_verticalInput * motorForce;
	}

	private void Brake()
	{
		if (m_break && rl.rpm > 1 && rr.rpm > 1) {
			rl.brakeTorque = mass * Mathf.Abs(rl.rpm) * breakSpeed * rl.mass * 4;
			rr.brakeTorque = mass * Mathf.Abs(rr.rpm) * breakSpeed * rr.mass * 4;
		} else if (m_break && rl.rpm <= 1 && rr.rpm <= 1) {
			rl.brakeTorque = mass * breakSpeed * rl.mass * 4;
			rr.brakeTorque = mass * breakSpeed * rr.mass * 4;
					
		} else {
			rl.brakeTorque = 0;	
			rr.brakeTorque = 0;
		}
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(fl, flT);
		UpdateWheelPose(fr, frT);
		UpdateWheelPose(rl, rlT);
		UpdateWheelPose(rr, rrT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

    public float CurrentSpeed
    {
        get { return Player.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;}
    }

	void Update()
    {
		speed.text = Mathf.RoundToInt(CurrentSpeed).ToString();

        if(CurrentSpeed > SpeedLim) {
            rb.drag = 2;
        } else {
            rb.drag = 0;
        }
	}

	
	
}
