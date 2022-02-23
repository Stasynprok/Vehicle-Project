using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCol : MonoBehaviour
{
    public float speed;
    public bool trig;
    public ConfigurableJoint ConfJoint;
    public Rigidbody _rigidbody;
    public Transform _rigidbody2;
    public Vector3 rot;
    public float angle = 10;
    public float torque = 1;
    public Vector3 rel;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ConfigurableJoint ConfJoint = GetComponent<ConfigurableJoint>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion deltarot = Quaternion.Euler((angle) * Input.GetAxis("Horizontal") * Vector3.up);
        //torque = _rigidbody.transform.position.rotation.y;

        if (trig == true){
            // rot.Set(0f, angle,0f);
            // rot = rot.normalized * Input.GetAxis("Horizontal");
            // Quaternion deltarot = Quaternion.Euler(rot);

            // rot = transform.localEulerAngles;
            ConfJoint.targetRotation = deltarot;
            //rel = transform.InverseTransformDirection(Vector3.up);
            //transform.rotation = Quaternion.AngleAxis(angle * Input.GetAxis("Horizontal"), rel);
            //_rigidbody.rotation = deltarot;
           // _rigidbody.MoveRotation(deltarot * Input.GetAxis("Horizontal"));
           //transform.localEulerAngles = new Vector3(0, angle * Input.GetAxis("Horizontal"), 0);
        }
        //_rigidbody.AddRelativeTorque(speed * Input.GetAxis("Vertical"), 0f, 0f, ForceMode.Force);
        
    }
}
