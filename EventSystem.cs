using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

    [SerializeField] public TransmissionScript transmission;
    [SerializeField] public FilterScript filter;
    [SerializeField] public RadiatorScript radiator;
    [SerializeField] public RefinerScript refiner;
    [SerializeField] private float timer;
    [SerializeField] private float eventInterval;
    [SerializeField] private float eventDamage;
    [SerializeField] private float timeIncreaseSpeed;

    // Use this for initialization
    void Start () {

        timer = 0;
        eventInterval = 40;
        eventDamage = 30;
        timeIncreaseSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {

        timer += timeIncreaseSpeed * Time.deltaTime;

        if(timer > eventInterval)
        {
            timer = 0;
            int rng = Random.Range(1, 101); //RNG for 100%

            if (rng > 0 && rng <= 20)
            {
                filter.DecreaseLife(eventDamage); //20% chance every interval for filter to take damage
            }

            else if (rng > 20 && rng <= 30)
            {
                transmission.DecreaseLife(eventDamage); //10% chance every interval for transmission to take damage
            } 

            else if (rng > 30 && rng <= 60)
            {
                radiator.DecreaseLife(eventDamage); //30% chance every interval for radiator to take damage
            }

            else if (rng > 60 && <= 90)
            {
                refiner.DecreaseLife(eventDamage); //30% chance every interval for radiator to take damage
            }

            //10% chance for nothing to happen
        }
	}
}
