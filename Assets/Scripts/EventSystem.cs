using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

    [SerializeField] float eventInterval = 30f;

    [SerializeField] GameManager manager;
    [SerializeField] TransmissionScript transmission;
    [SerializeField] FilterScript filter;
    [SerializeField] RadiatorScript radiator;
    [SerializeField] RefinerScript refiner;
    [SerializeField] CoreScript core;

    private float timer;

    // Use this for initialization
    void Start () 
    {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (!manager.IsTutorial())
        {
            timer += 1 * Time.deltaTime;

            if (timer >= eventInterval)
            {
                int rng = Random.Range(1, 101);

                if(rng > 0 && rng <= 25)
                {
                   Debug.Log("FILTER DAMAGED");
                   filter.DecreaseLife(30);
                }
                else if(rng > 25 && rng <= 50)
                {
                    Debug.Log("TRANSMISSION DAMAGED");
                    transmission.DecreaseLife(30);
                }
                else if(rng > 50 && rng <= 75)
                {
                   Debug.Log("RADIATOR DAMAGE");
                   radiator.DecreaseLife(30);
                }
                else
                {
                    Debug.Log("REFINER DAMAGED");
                    refiner.DecreaseLife(30);
                }

                timer = 0;
            }
        }
	}

    public void SetInterval(float interval)
    {
        eventInterval = interval;
    }
}
