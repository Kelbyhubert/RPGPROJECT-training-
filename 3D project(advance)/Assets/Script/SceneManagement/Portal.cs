using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour {
            // enum buat destinasi
            enum destinationIdentifier{
                A,B,C,D,E
            }
            [Header("Portal Property")]
            // [SerializeField] String portalName;
            [SerializeField] int sceneIndex = -1;
            [SerializeField] Transform spawnPoint;
            // [SerializeField] String destinationName;
            [SerializeField] destinationIdentifier destination;

            [Header("transistion Effect")]
            [SerializeField] float fadeInTime = 2f;
            [SerializeField] float fadeOutTime = 3f;
            [SerializeField] float fadeDelay = 0.5f;
            


            private void OnTriggerEnter(Collider other) {
            // jika player masuk ke area trigger baru akan melakukan method dengan StartCoroutine karena method nya IEnumerator
            if(other.tag == "Player"){
                    StartCoroutine(transition());
                }
            }

            private IEnumerator transition(){
            // jangan hancurkan gameObject portal dari scene sebelumnya
            // akan melakukan fade out dari class fader
            // scene akan berganti ke scene selanjutanya dengan SceneManager.LoadSceneAsync() / SceneManager.LoadScene() 
            // dari gameobject portal sebelumnya kita akan menjalankan beberapa method untuk pengambilan data
            // untuk ambil posisi dari spawn point dengan getOtherPortal()
            // jika sudah mengambil komponen portal dari scene sekarang maka akan memulai method updatePlayerPosition ke spawnPoint
            // menunggu selama fade delay baru menjalakan kan fadeIN
            // jika semua pemindahan data sudah selesai atau semua data yang diperlukan sudah diambil baru hancurkan gameObject portal dari scene sebelumnya

                Fader fader = FindObjectOfType<Fader>();

                DontDestroyOnLoad(gameObject);

                yield return fader.fadeOut(fadeOutTime);
                yield return SceneManager.LoadSceneAsync(sceneIndex); 

                Portal otherPortal = getOtherPortal();
                updatePlayerPosition(otherPortal);

                yield return new WaitForSeconds(fadeDelay);
                yield return fader.fadeIn(fadeInTime);

                Destroy(gameObject);
            }

        private void updatePlayerPosition(Portal otherPortal)
        {
            // mencari gameObject dengan tag Player 
            // posisi player diubah sesuai dengan spawnPoint dari data yang diambil oleh gameObject Portal scene sebelumnya
            
            GameObject player = GameObject.FindWithTag("Player");

            // ini cara  dengan mengambil komponen NavMeshAgent dari player dengan Warp ke posisi spawn Point
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            // ini cara tanpa navmeshAgent
            // player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal getOtherPortal()
        {
            // ini untuk mencari gameObeject yang ada komponen Portal untuk diambil Komponen Portalnya
            // karena di dalam Portal sebelum nya belum dihancur kan kita perlu memakai FindObjectsOfType<Portal>() untuk mencari semua yang ada komponen portal (bentuknya array)
            // jika portal tersebut bukan komponen portal scene sebelumnya maka akan di skip dengan continue
            // jika destination portal scene sebelumnya tidak sama dengan portal scene sekarang maka akan diskip dengan continue
            // jika portal dari scene sebelumnya maka akan direturn portalnya
            // jika tidak ada maka return null
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                // if(portal.destinationName != this.portalName) continue;
                if(portal.destination != this.destination) continue; 

                return portal;
            }
            return null;
        }
    }
    
}
