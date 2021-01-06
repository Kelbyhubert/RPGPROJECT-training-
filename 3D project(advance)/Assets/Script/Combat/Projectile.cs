using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health target = null;
        float damage = 0;
        [SerializeField] float speed = 2f;


        // ini untuk jalannya arrow ke target
        private void Update() {

            if(target == null) return;
            transform.LookAt(getTargetPosition());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // set target ke target yang diclick
        public void setTarget(Health target , float damage){
            this.target = target;
            this.damage = damage;
        }

        private Vector3 getTargetPosition()
        {
            CapsuleCollider cCollider = target.GetComponent<CapsuleCollider>();
            if(cCollider == null){
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * cCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Health>() != target) return;
            target.takeDamage(damage);
            Destroy(gameObject);

        }
    }
    
}
