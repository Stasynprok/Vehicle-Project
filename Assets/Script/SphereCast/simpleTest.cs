using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleTest : MonoBehaviour
{
  [Header("Suspension")]
    public Rigidbody rb;
    public float wheelRadius;
    public float springElasticity;
    public float damperElasticity;
    public float restLength;
    public float maxDelta;

    private float springDelta;
    private float springLength;
    private float springForce;
    private float damperForce;
    private float suspensionForce;

    private float speed;
    private float prevLength;

    public GameObject obj, parent, colliderWheel;
    
    private GameObject obj2, cwm;
    private Rigidbody rbColl;

    public Vector3 pointforce;
    public Vector3 pointforce2;
    private float lastLenght;
    private RaycastHit[] pointTarg;
    

    void Start()
    {
        obj2 = Instantiate(obj, transform.position, transform.rotation);
        cwm = Instantiate(colliderWheel, transform.position, transform.rotation);
        rbColl = cwm.GetComponent<Rigidbody>();

        obj2.transform.SetParent(parent.transform);
        cwm.transform.SetParent(parent.transform);
        obj2.transform.localScale = new Vector3(wheelRadius,wheelRadius,wheelRadius);
        cwm.transform.localScale = new Vector3(wheelRadius,wheelRadius,wheelRadius);
    }

    void FixedUpdate()
    {
        if (rbColl.SweepTest(-transform.up, out RaycastHit hit, restLength)){
          pointTarg = rbColl.SweepTestAll(-transform.up, restLength);
           // lastLenght = springLength;
            springLength = pointTarg[0].distance;
            springLength = Mathf.Clamp(springLength, springLength - maxDelta, springLength + maxDelta);
            springDelta = (restLength - springLength);
            springForce = springElasticity * springDelta;

            speed = (springLength - prevLength) / Time.fixedDeltaTime;
            damperForce = damperElasticity * speed;

            suspensionForce = springForce - damperForce;

            pointforce = transform.position + transform.up * pointTarg[0].point.y;
      
          //  parent.transform.position -  

            rb.AddForceAtPosition(transform.up * suspensionForce, pointforce);

            Debug.DrawRay(transform.position, -transform.up.normalized * springLength);
            obj2.transform.position = transform.position - transform.up * (springLength);
    
            prevLength = springLength;
            
        }
        // if(Physics.Raycast(transform.position, -transform.up,out RaycastHit hit, restLength + wheelRadius + maxDelta))
        // {
        //     point = transform.InverseTransformPoint(colW.ClosestPointOnBounds(parent.transform.position));
        //     springLength = Vector3.Distance(point, parent.transform.position);
        //     springLength = Mathf.Clamp(springLength, springLength - maxDelta, springLength + maxDelta);
        //     springDelta = restLength - springLength;
        //     springForce = springElasticity * springDelta;

        //     speed = (springLength - prevLength) / Time.fixedDeltaTime;
        //     damperForce = damperElasticity * speed;

        //     suspensionForce = springForce - damperForce;

        //     rb.AddForceAtPosition(transform.up * suspensionForce, hit.point);

        //     Debug.DrawRay(transform.position, -transform.up.normalized * springLength);
        //     obj2.transform.position = transform.position - transform.up * (springLength);
    
        //     prevLength = springLength;
        // }
    }
    
}
