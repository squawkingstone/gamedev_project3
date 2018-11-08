using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerScript : ChargeStation
{
    [SerializeField] GameManager manager;
    [SerializeField] TransmissionScript transmission;
    // [SerializeField] private float systemlife; //How much HP the system has
    // [SerializeField] private float repair; //How long it takes for the system to be repaired in seconds if it's offline
    // [SerializeField] private bool offline = false; //Determines whether or not system is offline if its HP reaches 0
    // [SerializeField] private bool powerloss; //Determinnes whether or not system has lost power
    // [SerializeField] private bool powered; //Determines whether or not the transmission is sending power to the system


    // Use this for initialization
    void Start()
    {
        InitChargeStation();
    }

    // Update is called once per frame
    void Update() 
    {
        if (transmission.Powered() && IsOnline())
        {
            Charge();
        }
        else
        {
            Damage();
        }

    }

}
