using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PersistentobjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool isSpawned = false;
        
        private void Awake()
        {

            if (isSpawned) return;
            SpawnPersistentObject();

            isSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
    
}
