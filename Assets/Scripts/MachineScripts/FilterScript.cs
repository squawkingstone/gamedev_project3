using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// probably add some filter count thing in here so that based on the filterSupply, we can know how
// many filters are in the thing (I think it's only one)

public class FilterScript : Repairable {

    // public GameObject filter;
    // public GameObject eventsystem; //This is in charge of all connecting events
    // public GameObject filtercell;

    [SerializeField] float filterDecaySpeed;
    [SerializeField] Transform filterSlot;
    [SerializeField] GameManager manager;
    [SerializeField] TransmissionScript transmission;
    
    // float systemlife; //How much HP the system has
    Cell filter;
    // private bool offline = false; //Determines whether or not system is offline if its HP reaches 0

    // Use this for initialization
    void Start()
    {
        InitRepairable();
        filter = null; // should this be zero if there's no filter in it yet?
    }

    // Update is called once per frame
    void Update() 
    {
        if (!manager.IsTutorial())
        {
            if (transmission.Powered() && filter != null)
            {
                filter.DecreaseCharge(filterDecaySpeed * Time.deltaTime);
            }
            else
            {
                Damage();
            }
        }
    }

    // should probably only do this if there's no filter in the thing
    public bool InsertFilter(GameObject f)
    {
        if (filter == null)
        {
            filter = f.GetComponent<Cell>();
            filter.Attach(() => {RemoveFilter();});
            filter.transform.parent = filterSlot;
            filter.transform.localPosition = Vector3.zero;
            return true;
        }
        return false;
    }

    public void RemoveFilter()
    {
        filter = null;
    }

    public bool Filtered()
    {
        if (filter == null) 
            return false;
        else 
            return (filter.GetCharge() > 0f);
    }
}