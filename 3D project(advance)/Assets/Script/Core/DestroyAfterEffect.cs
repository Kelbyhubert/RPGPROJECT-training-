using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour {
        
        //jika sudah tidak ada particle maka hancur kan gameobject yang buat particle 
        // taruh di object yang ada partikel
        private void Update() {
            if(!GetComponent<ParticleSystem>().IsAlive()){
                Destroy(gameObject);
            }
        }

    }
    
}