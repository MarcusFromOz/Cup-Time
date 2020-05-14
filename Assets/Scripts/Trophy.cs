using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Trophy : MonoBehaviour
{
    Health player;
    [SerializeField] float healAmount = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Collided with trophy");

        player = other.GetComponent<Health>();

        // Add 50ish to health
        if (other.tag == "Player")
        { 
            player.HealDamage(healAmount); 
        }


        //Add to UI

        // Make the trophy disappear (with effects - over 2 seconds)

        // Check the win condition

        // throw Update some info to the canvas
    }

}
