using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float camSpeed = 3f;
    public float speedX = 360f;
    public float speedY = 240f;
    public float limitY = 40f;
    public enum cRM
    {
        Static, NoStatic
    }
    public cRM cameraRotationMode;

    public LayerMask obstacles;

    private float maxDistance;
    private float currentRotation;
    private Vector3 currentPosition;
    private Vector3 localPosition;
    private Vector3 startPosition;
    public float MinDist, CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;

    private Vector3 _position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Start()
    {
        localPosition = target.InverseTransformPoint(_position);
        maxDistance = Vector3.Distance(_position, target.position);
        startPosition = target.InverseTransformPoint(_position);
    }

    void LateUpdate()
    {
        _position = target.TransformPoint(localPosition);
        transform.LookAt(target);

        switch (cameraRotationMode)
        {
            case cRM.Static:
                if (Input.GetMouseButton(1))
                {
                    CameraRotaion();
                    ObstaclesReact();  
                }
                else
                {
                    currentPosition = target.TransformPoint(startPosition);
                    _position = currentPosition;
                    transform.LookAt(target);
                    ObstaclesReact();                  
                }
                break;

            case cRM.NoStatic:
                if (Input.GetMouseButton(1))
                {
                    CameraRotaion();
                    ObstaclesReact();
                }
                break;
        }
        localPosition = target.InverseTransformPoint(_position);
    }

    void CameraRotaion()
    {
        var ax = Input.GetAxis("Mouse X");
        var ay = Input.GetAxis("Mouse Y");

        if (ay != 0)
        {
            var tmp = Mathf.Clamp(currentRotation + ay * speedY * Time.deltaTime, -limitY, limitY);
            if (tmp != currentRotation)
            {
                var rot = tmp - currentRotation;
                transform.RotateAround(target.position, transform.right, rot);
                currentRotation = tmp;
            }
        }
        if (ax != 0)
        {
            transform.RotateAround(target.position, Vector3.up, ax * speedX * Time.deltaTime);
        }
    }

    void ObstaclesReact()
    {
        RaycastHit hit;
                    if(Physics.Raycast(target.position, transform.position - target.position, out hit, maxDistance, obstacles))
                    {
                        _position = hit.point;
                    }
    }
}
