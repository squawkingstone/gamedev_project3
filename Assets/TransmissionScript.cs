using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionScript : Repairable {

    public GameObject transmission;
    public GameObject filter;
    public GameObject radiator;
    public GameObject refiner;
    public GameObject eventsystem; //This is in charge of all connecting events
    public float systemlife; //How much HP the system has
    public float repair; //How long it takes for the system to be repaired in seconds if it's offline
    public bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    public bool damaged = false; //Determines whether or not system is damaged
    public float electricity; //Shows generator's power level (if level goes too high or too low, generator enters danger zone)
    public float surge; //How much power the generator gains or loses
    


    // Use this for initialization
    void Start()
    {
        systemlife = 100;
        repair = 6;
        electricity = 100;
        surge = 0;

    }

    // Update is called once per frame
    void Update()
    {

        electricity += surge * Time.deltaTime;
        if (electricity > 120 || electricity <= 80) //If power surge goes above 120 or below 80, the generator will start taking damage
        {
            damaged = true;
            Damage();
        }


    }

    private void Damage()
    {
        if (systemlife > 0)
        {
            if (electricity > 120 && electricity <= 140)
            {
                systemlife -= Time.deltaTime; //Systemlife will drain by 1 per second
            }
            else if (electricity > 60 && electricity <= 80)
            {
                systemlife -= Time.deltaTime; //Systemlife will drain by 1 per second
            }
            else if (electricity > 140 && electricity <= 160)
            {
                systemlife -= 2 * Time.deltaTime; //Systemlife will drain by 2 per second
            }
            else if (electricity > 40 && electricity <= 60)
            {
                systemlife -= 2 * Time.deltaTime; //Systemlife will drain by 2 per second
            }
            else if (electricity > 160 && electricity <= 180)
            {
                systemlife -= 3 * Time.deltaTime; //Systemlife will drain by 3 per second
            }
            else if (electricity > 20 && electricity <= 40)
            {
                systemlife -= 3 * Time.deltaTime; //Systemlife will drain by 3 per second
            }
            else
            {
                systemlife -= 4 * Time.deltaTime; //Systemlife will drain by 4 per second
            }
        }
    }

    public override void Repairing()
    {
        if (offline == true)
        {
            float repairtime = 0;
            repairtime += Time.deltaTime;
            if (repairtime == repair)
            {
                offline = false;
                systemlife = 100;
            }
        }
    }
}
