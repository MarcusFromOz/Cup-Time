using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        //** variables
        GameObject player;

        //** start and update
        private void Start()
        {
            //Populate the Event list
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            
            player = GameObject.FindWithTag("Player");
        }

        //** public methods


        //** private methods

        void DisableControl(PlayableDirector pd)
        {
            //Stop the player from doing any shenanigans he started 
            // to do before triggering the cutscene
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        


    }
}

