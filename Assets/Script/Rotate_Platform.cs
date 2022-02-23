using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Platform : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float speed;
        void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
         _rigidbody.AddRelativeTorque(0f, speed, 0f, ForceMode.Force);
    }
}
