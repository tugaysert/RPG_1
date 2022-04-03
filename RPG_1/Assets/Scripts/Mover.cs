using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;


    void Start()
    {
     

    }

    void Update()
    {

        
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();

       


        //Debug.DrawRay(lastRay.origin, lastRay.direction*100);
        //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //gameObject.GetComponent<NavMeshAgent>().destination = target.position;

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //hit variable store etmek icin out kelimesini kullaniorz
        //position, where the raycast hit? icin

        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            gameObject.GetComponent<NavMeshAgent>().destination = hit.point;

        }
    }
    //mover : in Player
    private void UpdateAnimator() { 
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);

    }
}
