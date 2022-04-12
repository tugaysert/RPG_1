using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    //yeni unity'de prefab scene fln oldugu icin executineditmode deprecate olmus
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        //SerializeField yapmak persistent safe yapacak
        //default value'ye set ettik ki, degisiklige ihtiyac yok denilebilsin fln
        [SerializeField] string uniqueIdentifier = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            print("Capturing state for" + GetUniqueIdentifier());
                return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            print("Restoring state for" + GetUniqueIdentifier());
            SerializableVector3 position = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector3();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            //prefab scene'lerde guid olu?mas?n? istemiyoruz
            //prefab scene'lerin "path"leri yoktur bu sebeple bu bool'u kullanabiliriz.
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier"); 

            if(string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
