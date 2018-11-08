using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionScript : Repairable 
{
    // public GameObject transmission;
    // public GameObject eventsystem; //This is in charge of all connecting events
    // [SerializeField] private float systemlife; //How much HP the system has
    // [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    // [SerializeField] private float electricity; //Shows transmission power level (if level goes too high or too low, generator enters danger zone)
    // [SerializeField] private float damage;
    // [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    // [SerializeField] private bool connected; //Determines whether or not system is sending power
    // [SerializeField] private bool disconnected; //Determines whether or not system is not sending power

    // Use this for initialization
    void Start()
    {
        InitRepairable();
        // connected = true;
        // disconnected = false;
    }

    // I don't think this makes sense, there probably should be some mechanism for wh
    // Update is called once per frame
    void Update()
    {
        // if(connected == true)
        // {
        //     disconnected = false;
        //     electricity = 100;
        // }
        // else
        // {
        //     disconnected = true;
        //     if(electricity > 0)
        //     {
        //         electricity -= 5 * Time.deltaTime;
        //     }
        //     if (electricity <= 80) //If power surge goes above 120 or below 80, the generator will start taking damage
        //     {
        //         Damage();
        //     }
        // }
    }

    public bool Powered()
    {
        return IsOnline();
    }
}
