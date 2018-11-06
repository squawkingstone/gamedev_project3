using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerScript : Repairable {

    public GameObject refiner;
    public GameObject eventsystem; //This is in charge of all connecting events
    public GameObject powercell;
    public GameObject transmission;
    public float systemlife; //How much HP the system has
    public float repair; //How long it takes for the system to be repaired in seconds if it's offline
    public bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    public bool shutdown = false; //Determinnes whether or not system has lost power
    public bool powered = true; //Determines whether or not the transmission is sending power to the system

    // Use this for initialization
    void Start()
    {
        systemlife = 100;
        repair = 6;

    }

    // Update is called once per frame
    void Update()
    {
        if (powered == true)
        {
            shutdown = false;
        }
        else
        {
            shutdown = true;
            Damage();
        }

    }

    private void Damage()
    {
        if (systemlife > 0)
        {
            systemlife -= Time.deltaTime * 10;
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
