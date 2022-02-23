using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

    public WheelCollider[] frontCols;
    public Transform[] dataFront;
    public WheelCollider[] backCol;
    public Transform[] dataBack;
    public Transform centerOfMass;

    public float maxSpeed = 30f;
    private float sideSpeed = 30f;
    public float breakSpeed = 100f;

  //  private Sounds sound;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
      //  sound = gameObject.GetComponent<Sounds>();
    }

    void Update()
    {
        /** Get axis **/
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        bool brakeButton = Input.GetButton("Jump");
        /** End get axis **/

        /** Motor **/
        backCol[0].motorTorque = vAxis * maxSpeed;
        backCol[1].motorTorque = vAxis * maxSpeed;
        /**End motor **/

        /** Brake **/
        if (brakeButton)
        {
            frontCols[0].brakeTorque = Mathf.Abs(frontCols[0].motorTorque) * breakSpeed;
            frontCols[1].brakeTorque = Mathf.Abs(frontCols[1].motorTorque) * breakSpeed;
        }
        else
        {
            frontCols[0].brakeTorque = 0;
            frontCols[1].brakeTorque = 0;
        }
        /** End brake **/

        /** Rotate **/
        frontCols[0].steerAngle = hAxis * sideSpeed;
        frontCols[1].steerAngle = hAxis * sideSpeed;
        /** End rotate **/

        /** Update graphics **/
        dataFront[0].Rotate(frontCols[0].rpm * Time.deltaTime, 0, 0);
        dataFront[1].Rotate(frontCols[1].rpm * Time.deltaTime, 0, 0);
        dataBack[0].Rotate(backCol[0].rpm * Time.deltaTime, 0, 0);
        dataBack[1].Rotate(backCol[1].rpm * Time.deltaTime, 0, 0);
        dataFront[0].localEulerAngles = new Vector3(dataFront[0].localEulerAngles.x, hAxis * sideSpeed, dataFront[0].localEulerAngles.z);
        dataFront[1].localEulerAngles = new Vector3(dataFront[1].localEulerAngles.x, hAxis * sideSpeed, dataFront[1].localEulerAngles.z);
        /*** End update graphics cols **/

        /** Skid **/
        WheelHit hit;
        if (backCol[0].GetGroundHit(out hit))
        {
            float vol = (Mathf.Abs(hit.sidewaysSlip) > .25f) ? hit.sidewaysSlip / 5 : 0;
           // sound.playSkid(vol);
        }
        /** End skid **/
    }
}
