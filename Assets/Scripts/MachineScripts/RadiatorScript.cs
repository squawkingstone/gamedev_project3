using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorScript : ChargeStation {

    // public GameObject radiator;
    // public GameObject eventsystem; //This is in charge of all connecting events
    // public GameObject coolingcell;
    [SerializeField] public TransmissionScript transmission;
    // [SerializeField] private float systemlife; //How much HP the system has
    // [SerializeField] private float temperature; //Temperature of the radiator (in celsius). If temperature rises too high, damage will go up
    // [SerializeField] private float heat; //How fast the temperature rises
    // [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    // [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    // [SerializeField] private bool burning; //Determines whether or not system is burning
    // [SerializeField] private bool powerloss; //Determines whether or not system has lost power
    // [SerializeField] private bool powered; //Determines whether or not the transmission is sending power to the system

    // Use this for initialization
    void Start () 
    {
        InitChargeStation();
        // systemlife = 100;
        // temperature = 20;
        // heat = 2;
        // repair = 4;
        // powered = true;
        // burning = false;
        // powerloss = false;
	}
	
	// Update is called once per frame
	void Update () {

        // if (transmission.Powered() == true)
        // {
        //     powered = true;
        // }
        // else
        // {
        //     powered = false;
        // }

        // if (systemlife <= 0)
        // {
        //     offline = true;
        //     heat = 0;
        //     burning = false;
        //     powerloss = false;
        //     powered = false;
        // }

        if(transmission.Powered() && IsOnline())
        {
            Charge();
        }
        else
        {
            Damage();
        }

	}

    // private void Overheat()
    // {
    //     if (systemlife > 0 && offline == false)
    //     {
    //         systemlife -= Time.deltaTime * Mathf.CeilToInt(Mathf.Clamp((temperature - 30) / 10f, 0f, 5f));
    //     }
    // }

}
