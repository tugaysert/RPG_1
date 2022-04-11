using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector director;
        GameObject player;

        private void Start()

        {
            player  = GameObject.FindWithTag("Player");
            director = GetComponent<PlayableDirector>();
           director.played += DisableControl;
           director.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector director)
        {

           
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
           player.GetComponent<PlayerController>().enabled = false;
            print("Scene triggered.");

        }

        void EnableControl(PlayableDirector director)
        {
           
            print("Scene is done");
            player.GetComponent<PlayerController>().enabled = true;
        }
       

    }
} 
