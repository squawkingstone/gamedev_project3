using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour {

    public GameObject core;
    public GameObject eventsystem; //This is in charge of all connecting events
    public GameObject powercell; //power
    public GameObject coolingcell;
    [SerializeField] public FilterScript filter;
    [SerializeField] private float systemlife; //How much HP the system has
    [SerializeField] private float temperature; //Temperature of the core (in celsius). If temperature rises too high, damage will go up
    [SerializeField] private float heat; //How fast the temperature rises
    [SerializeField] private float energy; //System's power
    [SerializeField] private float powersupply; //How much energy the system has
    [SerializeField] private float coolingsupply; //How much cooling the system has
    [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    [SerializeField] private bool burning = false; //Determines whether or not system is burning
    [SerializeField] private bool dirty = false; //Determines whether or not system's water is unfiltered
    [SerializeField] private bool powerloss = false; //Determines whether or not core has lost power
    [SerializeField] private bool cooled; //Determines whether or not a cooling cell is attached to the system
    [SerializeField] private bool powered; //Determines whether or not a power cell is attached to the system
    [SerializeField] private bool tutorial; //If tutorial = true, the system HP cannot go down


    // Use this for initialization
    void Start()
    {
        tutorial = true;
        cooled = false;
        powered = false;
        systemlife = 300;
        energy = 30;
        temperature = 20;
        heat = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(systemlife <= 0)
        {
            offline = true;
        }

        if (tutorial == true)
        {
            offline = false;
        }
        else
        {
            if (coolingsupply > 0)
            {
                cooled = true;
                burning = false;
                temperature = 20;
                heat = 0;
                coolingsupply -= 5 * Time.deltaTime;
                if (coolingsupply <= 0)
                {
                    cooled = false;
                }
            }
            else
            {
                cooled = false;
                if(offline == false)
                {
                    temperature += heat * Time.deltaTime; //Temperature will rise by the value of heat per second
                }
                heat = 2;
                if (temperature > 30) //If temperature is over 30 it will begin to take damage
                {
                    burning = true;
                    Overheat();
                }
            }

            if (powersupply > 0)
            {
                powered = true;
                energy = 30;
                powerloss = false;
                powersupply -= 5 * Time.deltaTime;
                if (powersupply <= 0)
                {
                    powered = false;
                }
            }
            else
            {
                powered = false;
                if(energy > 0)
                {
                    energy -= 2 * Time.deltaTime;
                }
                
                if (energy <= 0)
                {
                    powerloss = true;
                    EnergyLoss();
                }
            }

            if (filter.Filtered() == true)
            {
                dirty = false;
            }
            else
            {
                dirty = true;
                Dirtywater();
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

    private void EnergyLoss()
    {
        if (systemlife > 0)
        {
            systemlife -= 5 * Time.deltaTime;
        }
    }

    private void Dirtywater()
    {
        if (systemlife > 0)
        {
            systemlife -= 3 * Time.deltaTime;
        }
    }
}