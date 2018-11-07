using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerScript : Repairable {

    public GameObject refiner;
    public GameObject eventsystem; //This is in charge of all connecting events
    public GameObject powercell;
    [SerializeField] public TransmissionScript transmission;
    [SerializeField] private float systemlife; //How much HP the system has
    [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    [SerializeField] private bool powerloss; //Determinnes whether or not system has lost power
    [SerializeField] private bool powered; //Determines whether or not the transmission is sending power to the system

    // Use this for initialization
    void Start()
    {
        systemlife = 100;
        repair = 6;
        powered = true;
        powerloss = false;
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
            powerloss = false;
            powered = false;
        }

        if (powered == true)
        {
            powerloss = false;
        }
        else
        {
            powered = false;
            powerloss = true;
            Damage();
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
}
