using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    //anggap aja ini data buat database weapon
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create New Weapon", order = 0)]
    public class Weapon : ScriptableObject {

        [SerializeField] AnimatorOverrideController overrideAnimation = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponDamage;
        [SerializeField] float weaponRange;
        [SerializeField] float attactTime;
        [SerializeField] bool leftHandedWeapon = false;
        [SerializeField] Projectile projectile = null;

        public void spawnWeapon(Transform handRPosition, Transform handLPosition, Animator weaponAnimation){
            // jika tidak ada weapon prefab maka tidak akan melakukan method ini 
            // sama juga dengan weapon prefab jika tidak ada animasi maka tidak akan override animasi
            // jika ada dan weapon nya adalah lefthanded maka posisi yang diambil dalah handLPosition jika tidak maka handRPosition
            // habis itu buat gameobject ke dalam game
            if(weaponPrefab != null)
            {
                Transform handPosition = getTransform(handRPosition, handLPosition);

                Instantiate(weaponPrefab, handPosition);
            }
            var overrideController = weaponAnimation.runtimeAnimatorController as AnimatorOverrideController;
            if (overrideAnimation != null){
                weaponAnimation.runtimeAnimatorController = overrideAnimation;
            }else if (overrideController != null)
            {
                weaponAnimation.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private Transform getTransform(Transform handRPosition, Transform handLPosition)
        {
            // jika ada dan weapon nya adalah lefthanded maka posisi yang diambil dalah handLPosition jika tidak maka handRPosition
            Transform handPosition;
            if (leftHandedWeapon == true)
            {
                handPosition = handLPosition;
            }
            else
            {
                handPosition = handRPosition;
            }

            return handPosition;
        }

        public bool hasProjectile(){
            //return true kalau projectile tidak null
            return projectile != null;
        }

        public void launchProjectile(Transform handRPosition, Transform handLPosition , Health target){
            // method ini buat Instantiate arrow untuk mengikuti target dan arrow keluar dari posisi yang sudah ditentukan
            // set target sesuai dengan yang di click ( di playerCombat)
            Projectile Lprojectile = Instantiate(projectile,
                                                getTransform(handRPosition,handLPosition).position,
                                                Quaternion.identity);
            Lprojectile.setTarget(target,weaponDamage);
        }

        // buat ambil stat dari data senjata
        public float getDamage(){
            return weaponDamage;
        }

        public float getRange(){
            return weaponRange;
        }

        public float getAttackTime(){
            return attactTime;
        }
    }
    
}
