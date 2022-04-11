using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool alreadyTriggered = false;


        private void OnTriggerEnter(Collider other)
        {

            if(!alreadyTriggered && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggered = true;
            }
            
        }
    }

}