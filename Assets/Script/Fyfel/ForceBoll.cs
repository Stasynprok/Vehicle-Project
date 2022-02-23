using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBoll : MonoBehaviour
{

    public Rigidbody rb;
    public float force;
    private float xIn;
    private float yIn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xIn = Input.GetAxis("Horizontal");
        yIn = Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f)){ 
          rb.AddForceAtPosition(transform.position + (yIn * transform.forward * force) + (xIn * transform.right * force), hit.point);
          Debug.Log(xIn);
        }

        //transform.forward * force * yIn + transform.right*xIn
    }
}
