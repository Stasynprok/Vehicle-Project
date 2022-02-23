using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public SuspCar[] wheels;
    public GearBox gear;

    public enum wheelDrive 
    {
        AWD,
        RWD,
        FWD
    }
    public wheelDrive WD;

    [Header("Engine Set")]

    public float torque; //мощность двигателя
    public float rpm; // обороты двигателя

    public int min_rpm = 700; // обороты холостого хода
    public int max_rpm = 7000; // максимальные обороты двигателя
    public float imertialEng = 0.3f; // инерциальный момент двигателя
    public float back_torque = -100f; // момент сопротивлений в двигателе
    private float RPMtoRad; // Обороты в радианы
    private float RadtoRPM; // Радианы в обороты

    private float graphTorq; // График развития мощности двигателем

    private float angAccel; // ускорение

    private float EngAngVel = 0f; // движущая сила двигателя

    [Header("Wheel")]
    public float[] driveTorque;
    public float wheelInert;
    public float[] wheelAnggVel;

    public float maxWheelSpeed;

    [Range (0f, 1)]public float diffCoefF_toR; 
    private float diffCF;
    private float diffCR;


    // inputs

    private float xInp; // вход горизонтали



    public float Force = 10f;

    public void Start() {
        RPMtoRad = (2*Mathf.PI)/60;
        RadtoRPM = 1/RPMtoRad;

        diffCF = 1 - diffCoefF_toR;
        diffCR = 0 + diffCoefF_toR;        
    }

    public void FixedUpdate() {
        EngineAngVel();
        DriveCh();

    }

    private void EngineAngVel(){

        xInp = Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f);

        graphTorq = - ((Mathf.Pow(rpm-9000, 2 ) / 324000 ) - 250);
        torque = Mathf.Lerp(back_torque, graphTorq * xInp, xInp);

        angAccel = torque / imertialEng;

        EngAngVel = Mathf.Clamp(EngAngVel + angAccel * Time.fixedDeltaTime, min_rpm * RPMtoRad, max_rpm * RPMtoRad);

        rpm = EngAngVel * RadtoRPM;
    }

    private void DriveCh(){
        foreach (SuspCar s in wheels){

            switch(WD){
            case wheelDrive.AWD:
            driveTorque[0] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCF * 0.5f;
            driveTorque[1] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCF * 0.5f;
            driveTorque[2] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCR * 0.5f;
            driveTorque[3] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCR * 0.5f;
            if(s.wheelFrontLeft) {
                s.wheelRot = wheelAnggVel[0];
                s.WheelTorque = driveTorque[0];
            }
             if(s.wheelFrontRight) {
                s.wheelRot = wheelAnggVel[1];
                s.WheelTorque = driveTorque[1];
            }
             if(s.wheelRearLeft) {
                s.wheelRot = wheelAnggVel[2];
                s.WheelTorque = driveTorque[2];
            }
             if(s.wheelRearRight) {
                s.wheelRot = wheelAnggVel[3];
                s.WheelTorque = driveTorque[3];
            }
            break;

            case wheelDrive.RWD:
            driveTorque[0] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * 0.1f * 0.5f;
            driveTorque[1] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * 0.1f * 0.5f;
            driveTorque[2] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCR * 0.5f;
            driveTorque[3] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * diffCR * 0.5f;
            if(s.wheelFrontLeft) {
                s.wheelRot = wheelAnggVel[0];
                s.WheelTorque = driveTorque[0];
                s.trRFWD = 1;
            }
             if(s.wheelFrontRight) {
                s.wheelRot = wheelAnggVel[1];
                s.WheelTorque = driveTorque[1];
                s.trRFWD = 1;
            }
             if(s.wheelRearLeft) {
                s.wheelRot = wheelAnggVel[2];
                s.WheelTorque = driveTorque[2];
            }
             if(s.wheelRearRight) {
                s.wheelRot = wheelAnggVel[3];
                s.WheelTorque = driveTorque[3];
            }
            break;

            case wheelDrive.FWD:
            driveTorque[0] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * 0.5f;
            driveTorque[1] = Mathf.Clamp(torque, 0f, 1000f) * gear.totalGearRatio * 0.5f;
            driveTorque[2] = 0f;
            driveTorque[3] = 0f; 
            if(s.wheelFrontLeft) {
                s.wheelRot = wheelAnggVel[0];
                s.WheelTorque = driveTorque[0];
                s.trRFWD = 1;
            }
             if(s.wheelFrontRight) {
                s.wheelRot = wheelAnggVel[1];
                s.WheelTorque = driveTorque[1];
                s.trRFWD = 1;
            }
             if(s.wheelRearLeft) {
                s.wheelRot = wheelAnggVel[2];
                s.WheelTorque = driveTorque[2];
            }
             if(s.wheelRearRight) {
                s.wheelRot = wheelAnggVel[3];
                s.WheelTorque = driveTorque[3];
            }
            break;
            }

            wheelInert = s.wheelMass * Mathf.Pow(s.wheelRadius, 2) * 0.5f;
            for (int i = 0;  i <= wheelAnggVel.Length - 1; i++){
                wheelAnggVel[i] += (driveTorque[i] / wheelInert) * Time.fixedDeltaTime;
            }

            if (gear.gear != 1){
                maxWheelSpeed = EngAngVel / gear.totalGearRatio;
            } else {
                maxWheelSpeed = float.PositiveInfinity;
            }

            for(int i = 0; i <= wheelAnggVel.Length - 1; i++){
                wheelAnggVel[i] = Mathf.Min( Mathf.Abs(wheelAnggVel[i]), Mathf.Abs(maxWheelSpeed) ) * Mathf.Sign(maxWheelSpeed);
            }       
        }
    }
}
