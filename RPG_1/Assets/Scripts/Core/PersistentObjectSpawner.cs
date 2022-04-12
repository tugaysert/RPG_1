using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        //persistenObjectPrefab'? daha önce yaratmad???m?z? nereden bilece?iz?
        //static variable ile:    

        //not a way of getting access to the persistent objects
        //we have to rely on things like find objects or tag etc.
        static bool hasSpawned = false;

        private void Awake() 
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();
            hasSpawned = true;
        }  

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
