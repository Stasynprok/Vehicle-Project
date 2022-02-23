using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject obj, parent;
    private GameObject obj2;

    public float distance;
    public Mesh rbM;
    public Vector3 point;
    public float springLength;
    public Collider colW;

     
    void Start()
    {
        obj2 = Instantiate(obj, transform.position, transform.rotation);
        rb = obj2.GetComponent<Rigidbody>();
        //rbM = obj2.GetComponent<MeshFilter>().sharedMesh;
        obj2.transform.SetParent(parent.transform);
        colW = obj2.GetComponent<Collider>();
        //Debug.Log(contact);
        
    }

    
    void Update()
    {
        if (rb.SweepTest(-transform.up, out RaycastHit hit, distance)){
            Debug.Log(hit.distance);
        }
        

        obj2.transform.position = transform.position; // перемещение модели колеса 
    }
   
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        
        if(Application.isPlaying){
            Gizmos.DrawWireSphere(transform.position - transform.up * distance, 1f);
           // Gizmos.DrawWireSphere(transform.position - transform.up * (springLength), wheelRadius);
        }
    }
}
