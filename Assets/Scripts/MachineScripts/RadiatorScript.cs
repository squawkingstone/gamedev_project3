using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorScript : Repairable {

    public GameObject radiator;
    public GameObject eventsystem; //This is in charge of all connecting events
    public GameObject coolingcell;
    public GameObject transmission;
    public float systemlife; //How much HP the system has
    public float temperature; //Temperature of the radiator (in celsius). If temperature rises too high, damage will go up
    public float heat; //How fast the temperature rises
    public float repair; //How long it takes for the system to be repaired in seconds if it's offline
    public bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    public bool burning = false; //Determines whether or not system is burning
    public bool shutdown = false; //Determines whether or not system has lost power
    public bool powered = true; //Determines whether or not the transmission is sending power to the system

    // Use this for initialization
    void Start () {
        systemlife = 100;
        temperature = 20;
        heat = 1;
        repair = 4;

	}
	
	// Update is called once per frame
	void Update () {
        if(powered == true)
        {
            temperature = 20;
            heat = 0;
            shutdown = false;
        }
        else
        {
            shutdown = true;
            heat = 2;
            temperature += heat * Time.deltaTime; //Temperature will rise by the value of heat per second
            if (temperature > 30) //If temperature is over 30 it will begin to take damage
            {
                burning = true;
                Overheat();
            }
        }

	}

    private void Overheat()
    {
        if (systemlife > 0)
        {
            systemlife -= Time.deltaTime * Mathf.CeilToInt(Mathf.Clamp((temperature - 30) / 10f, 0f, 5f));
        }
    }

    public override void Repairing()
    {
        if(offline == true)
        {
            float repairtime = 0;
            repairtime += Time.deltaTime;
            if(repairtime == repair)
            {
                offline = false;
                systemlife = 100;
            }
        }
    }
}
