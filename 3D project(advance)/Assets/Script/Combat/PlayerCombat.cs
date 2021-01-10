using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;
using UnityEngine;
using System;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    // harusnya nama class jadi combatMechanic 
    // class ini untuk proses semua data yang masuk dari class control 
    public class PlayerCombat : MonoBehaviour , IAction , ISaveable {
        
        
        [Header("Weapon Setting")]
        [SerializeField] Weapon DefaultWeapon = null;
        [SerializeField] Transform hand_R_position = null;
        [SerializeField] Transform hand_L_position = null;
        
        

        //variable untuk test code attackComboAnimation();
        //List<string> animationList = new List<string>(new string[] {"attack1AnimTrigger","attack2AnimTrigger" });
        //int comboNumber = 0;

        // health dari component target bukan component dari player
        Health target;
        Weapon currentWeapon;

        // agar player atau musuh langsung serang 
        // bisa pake angka yang lebih dari attack time 
        // pakai mathf.infinity agar langsung buat menjadi lebih besar dari attactTime
        float lastSecondAttack = Mathf.Infinity;

        private void Awake() {
            //saat game mulai langsung pake weapon defaultWeapon
            // jika currentWeapon null ( maksudnya jika load dari save file dan currentWeapon itu null) maka akan pake DefaultWeapon
            if(currentWeapon == null){
                equipWeapon(DefaultWeapon);
            }
        }

        

        private void Update()
        {
            // jika ada target dan rangeCalculate nya false maka player bisa jalan
            // jika rangeCalculate true maka player akan berhenti
            // jika target null maka akan di return langsung tampa menjalankan sisanya
            // jika frame ke update maka lastSecond akan menjadi waktu dari time.deltatime
            // jika target sudah mati maka tidak akan melakukan serangan

            lastSecondAttack += Time.deltaTime;

            if(target == null) return;
            if(target.isDead()) return;

            if (!rangeCalculate())
            {
                // default untuk speed itu 1f
                GetComponent<mover>().moveTo(target.transform.position , 1f);
            }
            else
            {
                GetComponent<mover>().cancelAction();
                AttackAnimation();
            }
        }


        private bool rangeCalculate()
        {
            // hitung selisih jarak target dengan player 
            // jika lebih kecil dari range maka akan return true
            return Vector3.Distance(target.transform.position, transform.position) < currentWeapon.getRange();
        }

        public bool canAttack(GameObject combatTarget){

            //jika tidak ada gameObject maka akan return false
            //jika ada maka akan diambil komponen health nya dari gameobject
            // jika ada dan belom mati maka akan return true
            // jika target sudah mati maka akan return false
            if(combatTarget == null) return false;

            Health tempTarget = combatTarget.GetComponent<Health>();
            return tempTarget != null && !tempTarget.isDead();
        }

        public void attack(GameObject target){
            // untuk memhentikan current action dan lalu menganti action menjadi action class ini
            // lalu memulai action di class ini 
            // memasukan data target dari gameobject ke dalam target di class ini dengan mengambil komponen health karena dataype untuk target pada class ini adalah health
            // mencari target yang memiliki component CombatTarget
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.GetComponent<Health>();
        }


        public void cancelAction()
        {
            //dari interface
            cancelAttack();
        }


        public void equipWeapon(Weapon EWeapon)
        {
            // jika ada senjata baru yang dipake maka currentWeapon akan diganti menjadi yang baru dipakai (script ambil weapon di tanah ada di PickUpItem)
            // jika ada maka akan dispawn di posisi tangan dan ngeoveride animasi serang
            currentWeapon = EWeapon;
            Animator anim = GetComponent<Animator>();
            EWeapon.spawnWeapon(hand_R_position,hand_L_position,anim);
            
        }

        public Health getTarget(){
            return target;
        }


        #region animation
        //animation
        public void cancelAttack(){
            // reset target ke null
            // jika dibatalkan maka animasi serang akan di reset ( bug kecil )
            GetComponent<Animator>().ResetTrigger("attackAnimTrigger");
            GetComponent<Animator>().SetTrigger("cancelAttackTrigger");
            GetComponent<mover>().cancelAction();
            this.target = null;
        }

        private void AttackAnimation()
        {
            // lookat agar player selalu hadap ke target saat melakukan attack
            // jika lastSecondAttack lebih besar dari attack time maka akan melakukan animasi
            //trigger method hit yang sudah ditaruk kedalam animasi
            // jika sudah melakukan animasi maka lastSecondAttack akan diubah ke 0 agar menjadi lebih kecil dari attack time
            // method ini bisa dipake dalam game fps seperti firerate pada senjata atau bisa dipakai sebagai cooldown dari suatu skill
            // jika menyerang maka cancelattack akan di reset ( bug kecil )
            transform.LookAt(target.transform);
            if (lastSecondAttack > currentWeapon.getAttackTime())
            {
                GetComponent<Animator>().ResetTrigger("cancelAttackTrigger");
                GetComponent<Animator>().SetTrigger("attackAnimTrigger");
                lastSecondAttack = 0;
            }

            
        }


        // test code untuk combo
        // private void ComboAttackAnimation()
        // {
        //     if (lastSecondAttack > attactTime)
        //     {
        //         while (comboNumber != animationList.Count)
        //         {
        //             GetComponent<Animator>().SetTrigger(animationList[comboNumber]);
        //             comboNumber++;
        //             lastSecondAttack = 0;
        //         }
        //         if (comboNumber == animationList.Count)
        //         {
        //             comboNumber = 0;
        //         }
        //     }
        // }

        //method dari animasi mukul
        void Hit(){
            // jika tidak ada target maka tidak akan melakukan method takeDamage (untuk menhindari null object exception doang (bug))
            // jika tidak ada projectile maka tidak akan memlakukan launchProjectile
            // jika animasi sudah sampai di durasi hit baru melakukan damage
            // mengambil komponen health dari target untuk melakukan method takeDamage
            // whoattack akan mengambil gameobject(tuju ke gameobject sendiri) yang sedang menyerang target
            if(target == null) return;
            if(currentWeapon.hasProjectile())
            {
                // mengambil data combat dari posisi tangan dan target
                currentWeapon.launchProjectile(hand_R_position,hand_L_position,target , gameObject);
            }
            else
            {
                target.takeDamage(currentWeapon.getDamage() , gameObject);
            }
        }
        
        //method dari animasi memanah
        void Shoot(){
            Hit();
        }


        #endregion
        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            //harus buat folder resourses dan semua scripable object masukan ke folder tersebut 
            // hanya scripable object karena ntr scripable object akan sendiri memanggil animasi dan attribute dia sendiri
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            equipWeapon(weapon);
        }
    }

    
}