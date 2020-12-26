using System;
using System.Collections;
using System.Collections.Generic;

using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
public class mover : MonoBehaviour , IAction
{
    
    [SerializeField] Transform targetPos;
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        updateAnimation();
    }

    private void updateAnimation()
    {
        //ambil velocity global dari Navmesh
        //ubah jadi local karena animasi hanya butuh tau kalau di jalan atau enggak (inverse)
        //udah vector local velocity ke float
        //pake setfloat untuk trigger animasi

        Vector3 globalVelocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity);
        float triggerSpeed = localVelocity.z;

        GetComponent<Animator>().SetFloat("ForwardSpeed", triggerSpeed);
    }

    public void cancelAction(){
        // buat movement nya berhenti
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void startMove(Vector3 destinations){
        
        // untuk memhentikan current action dan lalu menganti action menjadi action class ini
        // lalu memulia action di class ini 
        GetComponent<ActionScheduler>().StartAction(this);
        
        moveTo(destinations);
    }

    public void moveTo(Vector3 destinations){
        // buat move berjalan
        GetComponent<NavMeshAgent>().destination = destinations;
        GetComponent<NavMeshAgent>().isStopped = false;
    }
}
    
}


