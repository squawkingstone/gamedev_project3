using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

    public GameObject eventsystem;
    [SerializeField] public TransmissionScript transmission;
    [SerializeField] public FilterScript filter;
    [SerializeField] public RadiatorScript radiator;
    [SerializeField] public RefinerScript refiner;
    [SerializeField] public GameObject core;
    [SerializeField] private float timer;

    // Use this for initialization
    void Start () {

        timer = 0;

	}
	
	// Update is called once per frame
	void Update () {

        timer += 1 * Time.deltaTime;

        if(timer >= 120) //Reset timer every 2 minutes so it doesn't go to infinity
        {
            timer = 0;
        }

        if(timer % 40 == 0)
        {
            int rng = Random.Range(1, 101);
            if(rng > 0 && rng <= 25)
            {
                filter.DecreaseLife(30);
            }
            if (rng > 25 && rng <= 50)
            {
                transmission.DecreaseLife(30);
            }
            if (rng > 50 && rng <= 75)
            {
                radiator.DecreaseLife(30);
            }
            if (rng > 0 && rng <= 25)
            {
                refiner.DecreaseLife(30);
            }
        }

        if(timer % 60 == 0)
        {
            int rng = Random.Range(1, 201);
            if (rng > 0 && rng <= 25)
            {
                
            }
        }

	}
}
