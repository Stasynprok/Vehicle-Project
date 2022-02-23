using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class AntiStuckMy : MonoBehaviour
{
     [Space(15)]
    public bool useWheelAntiStuck = true;
    [Range(2, 360)]
    public int raysNumber = 36;
    [Range(0f, 360f)]
    public float raysMaxAngle = 180f;
    [Range(0f, 1f)]
    public float wheelWidth = .25f;
    [Space(15)]
    public Transform wheelModel;

    private WheelCollider _wheelCollider;
    private CarControl carController;
    private float orgRadius;
    public float radcir = 0.35f;
    public Vector3 point;
    public Vector3 centerPoint;
    public bool trig;

    void Awake()
    {
        _wheelCollider = GetComponent<WheelCollider>();
        carController = GetComponentInParent<CarControl>();
        orgRadius = _wheelCollider.radius;
        centerPoint = _wheelCollider.center;
    }
    
    void Update()
    {
        if (useWheelAntiStuck)
        {
            if (!wheelModel)
                return;

            float radiusOffset = 0f;          
               // _wheelCollider.center = centerPoint;
            
            if (Physics.SphereCast(wheelModel.position - (-transform.up) *_wheelCollider.radius, radcir - 0.01f, -transform.up, out RaycastHit hit, _wheelCollider.radius))
            {
                if (!hit.transform.IsChildOf(carController.transform) && !hit.collider.isTrigger)
                {
                    point = transform.InverseTransformPoint(hit.point);
                    Debug.DrawLine(wheelModel.position, hit.point, Color.red);
                    radiusOffset = Mathf.Max(radiusOffset, _wheelCollider.radius - hit.distance);
                    if (trig == true){
                        Debug.Log(point.ToString("F5"));

                    }
                }
            }
            _wheelCollider.center = new Vector3(point.x, -point.y + centerPoint.y, point.z);
            //_wheelCollider.radius = Mathf.LerpUnclamped(_wheelCollider.radius, orgRadius + radiusOffset, Time.deltaTime * 10f);
        }
        else
        {
            _wheelCollider.center = centerPoint;
           // _wheelCollider.radius = Mathf.LerpUnclamped(_wheelCollider.radius, orgRadius, Time.deltaTime * 10f);
        }
        
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        
        if(Application.isPlaying){
            Gizmos.DrawWireSphere(wheelModel.position, radcir);
           // Gizmos.DrawWireSphere(transform.position - transform.up * (springLength), wheelRadius);
        }
    }
}
