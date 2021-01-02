using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control
{
    // class ini berfungsi untuk input data ke dalam method dari class lain yang diperlukan untuk gameObject 
    public class AIControl : MonoBehaviour {
        
        [SerializeField] float triggerRange = 5f;
        [SerializeField] float searchForPlayerTime = 3f;

        [Header("path")]
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float wayPointDelayTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;


        PlayerCombat combatBehaviour;
        GameObject player;
        Health health;
        int currentWaypointIndex = 0;

        Vector3 guardPosition;
        float lookat;
        float timeLastSawPlayer = Mathf.Infinity;
        float wayPointDelay = Mathf.Infinity;

        private void Start() {
            // buat ambil component biar tidak kepanjangan di method
            combatBehaviour = GetComponent<PlayerCombat>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();

            guardPosition = transform.position;
            
            
        }


        private void Update()
        {
            // Time.deltaTime adalah jumlah detik yang dibutuhkan mesin untuk memproses frame sebelumnya.
            // Perhitungannya agak sederhana: menggunakan jam internal sistem untuk membandingkan waktu sistem 
            // saat mesin mulai memproses kerangka sebelumnya dengan waktu sistem saat mesin mulai memproses kerangka saat ini

            //jika player didalam range AI dan player tersebut bisa diserang maka akan melakukan attack ke player
            // karena cara serang AI enemy sama dengan cara serang Player maka bisa dipakai method yang di dalam class PlayerCombat
            // jika AI enemy sedang mengejar dan target AI keluar dari radius AI akan berhenti dalam waktu searchForPlayerTime yang ditentukan baru balik ke posisi awal jika timelastSawPlayer lebih besar dari searchForPlayerTime
            // searchBehaviour berfungsi untuk membuat AI mendelay balik ke posisi awal , jika ingin langsung balik ke posisi awal seachForPlayer di gameObject ubah jadi 0
            // jika inRange() && combatBehaviour.canAttack(player) maka timeLastSawPlayer akan tetap 0 jika tidak maka akan dijumlahkan dengan time.Deltatime
            //jika sudah tidak pada range atau sudah mati maka AI enemy akan balik ke posisi awal

            if (health.isDead()) return;

            if (inRange() && combatBehaviour.canAttack(player))
            {

                attackBehaviour();

            }
            else if (timeLastSawPlayer < searchForPlayerTime)
            {
                searchBehaviour();
            }
            else
            {
                PatrolBehaviour();
                

            }

            AITimer();

        }


        private void attackBehaviour()
        {
            timeLastSawPlayer = 0;
            combatBehaviour.attack(player);
        }

        private void searchBehaviour()
        {
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }

        // harap dibaca comment yang bagian PatrolBehaviour , atCurrentWaypoint , getCurrentPosition , loopWaypoint
        // bila tidak maka anda akan berak berak mengerti bagian Patrol

        private void PatrolBehaviour()
        {
            //mengambil posisi awal dari AI enemy
            // jika AI enemy ada object yang memiliki patrolPath maka maka AI enemy akan ikutin Path nya
            // jika tidak ada AI enemy akan balik ke posisi awal

            Vector3 nextPosition = guardPosition;

            if(patrolPath != null){

                //jika AttCurrentWaypoint return true maka akan melakukan loopWayPoint
                //jika tidak maka nextPosition akan di assign getCurrentPosition
                // jika AI enemy sudah mendekati dengan Waypoint yang dituju maka akan mengubah Index waypoint ke Index waypoint Selanjutnya
                // waypointDelay akan ke reset agar AI ada delay saat mau pindah
                if(atCurrentWaypoint()){
                    wayPointDelay = 0;
                    loopWaypoint();

                }
                nextPosition = getCurrentPosition();
            }

            // jika wayPointDelay lebih besar dari wayPointDelayTime AI enemy baru akan pindah
            // jika tidak AI akan tunggu sampai time.deltatime yang dijumlahkan ke wayPointDelay mencapai wayPointDelayTime
            // jika AI enemy lagi patrol maka speed yang akan dikali hanya dari 1 sampai 0
            if(wayPointDelay > wayPointDelayTime) GetComponent<mover>().startMove(nextPosition , patrolSpeedFraction);
        }

        private bool atCurrentWaypoint()
        {
            // jika selisih posisi AI enemy dan getCurrentPosition lebih kecil maka akan return true
            // ini untuk trigger kapan AI akan pindah ke wayPoint selanjutnya
            float distanceToWayPoint = Vector3.Distance(transform.position,getCurrentPosition());
            return distanceToWayPoint < wayPointTolerance;
        }

        private Vector3 getCurrentPosition()
        {
            // mengambil data posisi Waypoint 
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }


        private void loopWaypoint()
        {
            // untuk mengubah value currentWaypointIndex ke index selanjut nya 
            // patrolPath.GetNextWayPoint(currentWaypointIndex) adalah method buat penambahannya (bisa diliat di PatrolPath.cs)
            currentWaypointIndex = patrolPath.GetNextWayPoint(currentWaypointIndex);
        }

        private void AITimer()
        {
            // mulai hitung timer
            timeLastSawPlayer += Time.deltaTime;
            wayPointDelay += Time.deltaTime;
        }

        private bool inRange()
        {
            // hitung selisih range posisi object ini dengan object dengan tag Player
            // jika calculateDistance nya lebih kecil dari triggerRange maka return true jika tidak return false
            float calculateDistance = Vector3.Distance(player.transform.position, transform.position);

            return calculateDistance < triggerRange;
        }


        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,triggerRange);
        }
    }
    
}
