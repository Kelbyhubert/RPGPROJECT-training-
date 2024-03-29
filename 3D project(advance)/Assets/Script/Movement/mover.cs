using System;
using System.Collections;
using System.Collections.Generic;

using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    // class ini untuk proses semua data yang masuk dari class control 
    public class mover : MonoBehaviour , IAction , ISaveable
    {
    
    [SerializeField] Transform targetPos;
    [SerializeField] float MaxSpeed = 6f;
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        updateAnimation();
    }

    #region animation

    private void updateAnimation()
    {
        //ambil velocity global dari Navmesh
        //ubah jadi local karena animasi hanya butuh tau kalau di jalan atau enggak (inverse)
        //ubah vector local velocity ke float dengan ambil salah satu arahnya
        //pake setfloat untuk trigger animasi

        Vector3 globalVelocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity);
        float triggerSpeed = localVelocity.z;

        GetComponent<Animator>().SetFloat("ForwardSpeed", triggerSpeed);
    }

    #endregion

    #region function utama
    
    public void cancelAction(){
        // buat movement nya berhenti
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void startMove(Vector3 destinations , float fractionSpeed){
        
        // untuk memhentikan current action dan lalu menganti action menjadi action class ini
        // lalu memulia action di class ini 
        GetComponent<ActionScheduler>().StartAction(this);
        
        moveTo(destinations ,fractionSpeed);
    }

    public void moveTo(Vector3 destinations , float fractionSpeed){
            // buat move berjalan
            // ubah speed dari navMeshAgent
            // Mathf.Clamp01 berfungsi return value dari 1 sampai 0 
        GetComponent<NavMeshAgent>().destination = destinations;
        GetComponent<NavMeshAgent>().speed = MaxSpeed * Mathf.Clamp01(fractionSpeed);
        GetComponent<NavMeshAgent>().isStopped = false;
    }


        #endregion
    
    public object CaptureState()
    {
        // untuk mengambil data buat di Save
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        // mengubah value variable dari data yang di save file
            SerializableVector3 _position = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = _position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
             
    }

    }
    
}


