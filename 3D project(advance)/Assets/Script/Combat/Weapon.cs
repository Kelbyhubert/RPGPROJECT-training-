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

        public void spawnWeapon(Transform handPosition, Animator weaponAnimation){
            if(weaponPrefab != null){
                Instantiate(weaponPrefab,handPosition);
            }
            if(overrideAnimation != null){
                weaponAnimation.runtimeAnimatorController = overrideAnimation;
            }
        }

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
