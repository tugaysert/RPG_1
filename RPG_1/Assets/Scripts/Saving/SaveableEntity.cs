using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        


        [SerializeField] string uniqueIdentifier = "";

        //bu degiskeni sahne icerisinde kopyalanan gameobjelerin ayn? UUID'e sahip olmamas? icin yaratt?k.
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

      
        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();

            //SaveableEntity scriptinin attach oldu?u her bir gameobje içerisinde
            //ISaveable olan componentlar? buluyor (Mover,Health vs.) ve bir dictionary olarak kaydediyoruz.
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
               state[saveable.GetType().ToString()] = saveable.CaptureState();
            }   
            return state;
        }

        //SaveableEntity.cs
        public void RestoreState(object state)
        {
           
            Dictionary<string, object>  stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString)) saveable.RestoreState(stateDict[typeString]);
            }
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

            if(string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            //globalLookup static oldugu icin sorun yok 
            globalLookup[property.stringValue] = this;
        }


        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if ((globalLookup[candidate] == null)) 
            {
                
                globalLookup.Remove(candidate);
                return true;
            }

            if(globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;    
            
        }
#endif
    }
}
