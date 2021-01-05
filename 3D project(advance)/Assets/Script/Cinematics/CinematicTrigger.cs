using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour ,ISaveable
    {

        // [SerializeField] Collider[] enableNextCutscene;
        bool alreadyPlay = false;

        //buat boolean untuk cek timelinenya udah ke play atau belum
        //jika player masuk kotak collider dari scene maka akan play timeline dan timeline belom keplay sama sekali
        // pastikan play on awake di matikan jika ingin pake trigger ini
        // pastikan hanya bisa collide dengan player
        // jika sudah memainkan timeline buat boolean alreadyPlay jadi true sehingga tidak akan diplay lagi
        // bisa juga destroy dari durasi
        // selanjutnya akan dicari object yang ada component SavingWrapper
        // lalu melakukan Save 

        private void OnTriggerEnter(Collider other) {

            if(!alreadyPlay && other.gameObject.tag == "Player"){
                alreadyPlay = true;
             GetComponent<PlayableDirector>().Play();
            //  enableNextTimeline();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            }
        }

    // private void enableNextTimeline(){

    //     if(alreadyPlay == true){
    //         if(enableNextCutscene.Length == 0) return;
    //         if(enableNextCutscene == null) return;
    //         foreach (Collider cutscene in enableNextCutscene)
    //         {
    //             cutscene.enabled = true;   
    //         }
    //     }

    // }

    public object CaptureState()
    {
        // untuk mengambil data buat di Save
        return alreadyPlay;
    }

    public void RestoreState(object state)
    {
            
            // mengubah value variable dari data yang di save file
            this.alreadyPlay = (bool) state;
    }

    }
    
}

