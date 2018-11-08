using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionScript : Repairable {

    public GameObject transmission;
    public GameObject eventsystem; //This is in charge of all connecting events
    [SerializeField] private float systemlife; //How much HP the system has
    [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    [SerializeField] private float electricity; //Shows transmission power level (if level goes too high or too low, generator enters danger zone)
    [SerializeField] private float damage;
    [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    [SerializeField] private bool connected; //Determines whether or not system is sending power
    [SerializeField] private bool disconnected; //Determines whether or not system is not sending power

    // Use this for initialization
    void Start()
    {
        systemlife = 100;
        repair = 6;
        electricity = 100;
        connected = true;
        disconnected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(connected == true)
        {
            disconnected = false;
            electricity = 100;
        }
        else
        {
            disconnected = true;
            if(electricity > 0)
            {
                electricity -= 5 * Time.deltaTime;
            }
            if (electricity <= 80) //If power surge goes above 120 or below 80, the generator will start taking damage
            {
                Damage();
            }
        }
    }

    private void Damage()
    {
        if (systemlife > 0 && offline == false)
        {
            systemlife -= Time.deltaTime * damage;
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
                disconnected = false;
                connected = true;
                systemlife = 100;
                offline = false;
            }
        }
    }

    public bool Powered()
    {
        return connected;
    }
}
