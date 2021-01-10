using System;
using System.Collections;
using System.Collections.Generic;

using RPG.Core;
using RPG.Resources;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health target = null;
        float damage = 0;
        [SerializeField] float speed = 2f;
        [SerializeField] bool isFollowingTarget = false;
        [SerializeField] GameObject impactEffect = null;
        [SerializeField] GameObject[] impactObject = null;
        [SerializeField] float lifeTime = 10f;
        [SerializeField] float lifeAfterImpact = 2;
        GameObject whoAttack = null;

        private void Start() {
            transform.LookAt(getTargetPosition()); 
        }

        // ini untuk jalannya arrow ke target
        private void Update() {

            if(target == null) return;
            if(isFollowingTarget && !target.isDead()){
            transform.LookAt(getTargetPosition());

            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // set target ke target yang diclick
        public void setTarget(Health target , float damage , GameObject whoAttack){
            this.target = target;
            this.damage = damage;
            this.whoAttack = whoAttack;

            Destroy(gameObject,lifeTime);
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
            // jika collider yang disentuh bukan collider dari target maka tidak akan dihiraukan dan tetap kejer target
            // jika target mati juga tidak bisa diserang
            // jika sudah menyentuh target , target kena damage dengan method takedamage dari script health
            
            // speed diubah ke 0 jika udah damage
            // jika ada impact effect maka akan melakukan effect dulu
            // jika tidak maka akan destroy object projectile ini
            if(other.GetComponent<Health>() != target) return;
            if(target.isDead()) return;
            target.takeDamage(damage,whoAttack);

            speed = 0;
            if(impactEffect != null){
                Instantiate(impactEffect,getTargetPosition(),transform.rotation);
            }

            foreach (GameObject Destroyobject in impactObject)
            {
                Destroy(Destroyobject);
            }
            Destroy(gameObject,lifeAfterImpact);

        }
    }
    
}
