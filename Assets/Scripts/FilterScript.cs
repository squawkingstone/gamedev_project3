using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterScript : Repairable {

    public GameObject filter;
    public GameObject eventsystem; //This is in charge of all connecting events
    public GameObject filtercell;
    [SerializeField] public TransmissionScript transmission;
    [SerializeField] private float systemlife; //How much HP the system has
    [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    [SerializeField] private float filtersupply; //How much supply is in the filter cell
    [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    [SerializeField] private bool powerloss = false; //Determinnes whether or not system has lost power
    [SerializeField] private bool powered; //Determines whether or not the transmission is sending power to the system
    [SerializeField] private bool filtered;
    [SerializeField] private bool tutorial; //If tutorial = true, the system HP cannot go down

    // Use this for initialization
    void Start()
    {
        systemlife = 100;
        repair = 6;
        filtersupply = 100;
        tutorial = true;
        powered = true;
        filtered = true;
    }

    // Update is called once per frame
    void Update() {

        if (transmission.Powered() == true)
        {
            powered = true;
        }
        else
        {
            powered = false;
        }

        if (systemlife <= 0)
        {
            offline = true;
            powered = false;
            powerloss = false;
            filtered = false;
        }

        if (tutorial == true)
        {
            offline = false;
        }
        else
        {
            if (powered == true)
            {
                if (filtersupply > 0)
                {
                    filtered = true;
                    filtersupply -= 5 * Time.deltaTime; //How fast the cell loses supply
                }
                else
                {
                    filtered = false;
                }
                powerloss = false;
            }
            else
            {
                powerloss = true;
                Damage();
            }
        }
    }

    private void Damage()
    {
        if (systemlife > 0 && offline == false)
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
                systemlife = 100;
                offline = false;
            }
        }
    }

    public bool Filtered()
    {
        return filtered;
    }
}