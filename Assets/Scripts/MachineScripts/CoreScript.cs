using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour 
{
    [SerializeField] float maxLife = 300;
    [SerializeField] float temperatureDamageRate = 1;
    [SerializeField] float powerDamageRate = 1;
    [SerializeField] float filterDamageRate = 1;

    [SerializeField] float coolingLossRate = 1;
    [SerializeField] float powerLossRate = 1;

    [SerializeField] GameManager manager;
    [SerializeField] FilterScript filter;
    private float systemlife; //How much HP the system has
    private float totalPower; //How much energy the system has
    private float totalCooling; //How much cooling the system has
    private bool tutorial; //If tutorial = true, the system HP cannot go down

    // Use this for initialization
    void Start()
    {
        systemlife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsTutorial())
        {
            UpdateStatus();
            if (systemlife <= 0f)
            {
                GoOffline();
            }
            // Debug.Log(systemlife);
        }
    }

    void UpdateStatus()
    {
        UpdateCooling();
        UpdatePower();
        UpdateFilter();            
    }

    void UpdateCooling()
    {
        if (totalCooling > 0)
        {
            totalCooling -= coolingLossRate * Time.deltaTime;
        }
        else
        {
            TakeTemperatureDamage();
        }
    }

    void UpdatePower()
    {
        if (totalPower > 0)
        {
            totalPower -= powerLossRate * Time.deltaTime;
        }
        else
        {
            TakePowerDamage();
        }
    }

    void UpdateFilter()
    {
        if (!filter.Filtered())
        {
            TakeFilterDamage();
        }
    }

    public void AddCooling(float amount)
    {
        totalCooling += amount;
    }

    public void AddPower(float amount)
    {
        totalPower += amount;
    }

    private void TakeTemperatureDamage()
    {
        if (systemlife > 0)
        {
            systemlife -= temperatureDamageRate * Time.deltaTime;
        }
    }

    private void TakePowerDamage()
    {
        if (systemlife > 0)
        {
            systemlife -= powerDamageRate * Time.deltaTime;
        }
    }

    private void TakeFilterDamage()
    {
        if (systemlife > 0)
        {
            systemlife -= filterDamageRate * Time.deltaTime;
        }
    }

    /* Called when the system dies, should do the game over stuff or call something in a game manager or something */
    void GoOffline()
    {

    }
}