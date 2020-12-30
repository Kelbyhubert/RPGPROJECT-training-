using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        //buat boolean untuk cek timelinenya udah ke play atau belum
        //jika player masuk kotak collider dari scene maka akan play timeline dan timeline belom keplay sama sekali
        // pastikan play on awake di matikan jika ingin pake trigger ini
        // pastikan hanya bisa collide dengan player
        // jika sudah memainkan timeline buat boolean alreadyPlay jadi true sehingga tidak akan diplay lagi
        // bisa juga destroy dari durasi
        bool alreadyPlay = false;
        private void OnTriggerEnter(Collider other) {

            if(!alreadyPlay && other.gameObject.tag == "Player"){
            GetComponent<PlayableDirector>().Play();
            alreadyPlay = true;
            }
        }
    
    }
    
}

