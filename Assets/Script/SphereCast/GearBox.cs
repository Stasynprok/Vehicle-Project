using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBox : MonoBehaviour
{

    public float[] gearRatio;

    public int gear = 1;
    public float maingGear = 3.18f;
    public float effic = 0.8f;
    public float totalGearRatio;

    public float gearChTime;

    public int gPlus = 0;
    public int gMinus = 0;


    public Text gearNum;

    private int gearTr;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if ((gear < gearRatio.Length - 1) && Input.GetKey(KeyCode.Keypad6) && (gPlus == 0)) {
            StartCoroutine(GearUp());
            gPlus = 1;
        } else if (!Input.GetKey(KeyCode.Keypad6)){
            gPlus = 0;
        }
        if ((gear > 0) && Input.GetKey(KeyCode.Keypad3) && (gMinus == 0)) {
            StartCoroutine(GearDown());
            gMinus = 1;
        } else if (!Input.GetKey(KeyCode.Keypad3)){
            gMinus = 0;
        }

        totalGearRatio = gearRatio[gear] * maingGear;   

        if (gearNum != null) 
        {
               
            if(gear == 0) 
            {
                gearNum.text = "R";
            }
            else if (gear == 1)
            {
                gearNum.text = "N";
            }
            else if (gear >= 2)
            {
                gearNum.text = (gear-1).ToString();
            }

            //gearNum.text = gear.ToString();
        }

    }

    private IEnumerator GearUp(){
        gear = gear + 1;
        gearTr = gear;
        gear = 1;
        yield return new WaitForSecondsRealtime(gearChTime);
        gear = gearTr;
    }

    private IEnumerator GearDown(){
        gear = gear - 1;
        gearTr = gear;
        gear = 1;
        yield return new WaitForSecondsRealtime(gearChTime);
        gear = gearTr;
    }
}
