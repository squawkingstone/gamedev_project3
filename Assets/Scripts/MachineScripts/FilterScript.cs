using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// probably add some filter count thing in here so that based on the filterSupply, we can know how
// many filters are in the thing (I think it's only one)

public class FilterScript : Repairable {

    // public GameObject filter;
    // public GameObject eventsystem; //This is in charge of all connecting events
    // public GameObject filtercell;
    [SerializeField] float filterValue;

    [SerializeField] GameManager manager;
    [SerializeField] TransmissionScript transmission;
    
    // float systemlife; //How much HP the system has
    float filterSupply; //How much supply is in the filter cell
    // private bool offline = false; //Determines whether or not system is offline if its HP reaches 0

    // Use this for initialization
    void Start()
    {
        InitRepairable();
        filterSupply = 100; // should this be zero if there's no filter in it yet?
    }

    // Update is called once per frame
    void Update() 
    {
        if (!manager.IsTutorial())
        {
            if (transmission.Powered())
            {
                if (filterSupply > 0)
                {
                    filterSupply -= 5 * Time.deltaTime; //How fast the cell loses supply
                }
            }
            else
            {
                Damage();
            }
        }
    }

    // should probably only do this if there's no filter in the thing
    public void InsertFilter(float amount)
    {
        filterSupply += filterValue;
    }

    public void RemoveFilter()
    {
        // do the grab thing here...
    }

    public bool Filtered()
    {
        return (filterSupply > 0f);
    }
}