using UnityEngine;

public class CarAng : MonoBehaviour
{
    public SuspCar[] wheels; // массив для передачи показаний колесу

    [Header("Car Specs")]
    public float wheelBase; // колесная база в метрах
    public float rearTrack; // ширина задней колеи в метрах
    public float turnRadius; // радиус поворота в метрах

    private Rigidbody rb; // для расчета центра массы
    public Vector3 center_of_mass; // перемещение центра масс
    public Transform centerOfMassTransform; // точка центра масс на автомобиле

    [Header("Inputs")]

    public float steerInput; // ось поворота

    public float ackermannAngleLeft; // угол акермана лево
    public float ackermannAngleRight; // угол акермана право

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // получаем твердое тело
    }

    void OnDrawGizmos() // рисование центра масс
    {
        if (Application.isPlaying){
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(rb.worldCenterOfMass, 0.1f);
        }
    }

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal"); // получение значения по горизонтальной оси (отслеживанеи нажатия кнопки) для поворота

        rb.centerOfMass = Vector3.Scale(centerOfMassTransform.localPosition, transform.localScale); // задания центра масс для твердого тела

        if (steerInput > 0){ // поворот направо

            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0){ // поворот налево

            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;

        }
        else{ // елси клавиша поворота не нажата
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }

        foreach (SuspCar w in wheels){ // передача значений поворота только передним колёсам
            if (w.wheelFrontLeft){
                w.steerAngle = ackermannAngleLeft;
            }
            if (w.wheelFrontRight){
                w.steerAngle = ackermannAngleRight;
            }
        }
    }
}
