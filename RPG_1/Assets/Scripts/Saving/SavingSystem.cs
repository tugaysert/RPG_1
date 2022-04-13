using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using RPG.Core;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        
        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);

            if(state.ContainsKey("lastSceneBuilderIndex"))
            {
                int buildIndex = (int)state["lastSceneBuilderIndex"];
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }

            RestoreState(state);
        }
     
        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
            
        }

        

        public void Load(string saveFile)
        {

            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            //e?er yoksa bo? dictionary dön
            if (!File.Exists(path)) return new Dictionary<string, object>();

            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>) formatter.Deserialize(stream);

            }
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);

            }
        }

  
        private void CaptureState(Dictionary<string, object> state)
        {     
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();   
            }

            state["lastSceneBuilderIndex"] = SceneManager.GetActiveScene().buildIndex;
            
        }

        private void RestoreState(Dictionary<string, object> state)
        {
           
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();

                //eger dosya yoksa bos donecegimiz icin key bulunmayacak, exception atacak bu
                //sebeple alttaki if'e ihtiyac var
                if(state.ContainsKey(id)) saveable.RestoreState(state[id]);


            } 
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }

}
