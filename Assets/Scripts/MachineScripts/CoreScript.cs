using System;
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

    [SerializeField] Transform[] coolingCellSlots;
    [SerializeField] Transform[] powerCellSlots;

    [SerializeField] GameManager manager;
    [SerializeField] FilterScript filter;
    [SerializeField] MapSystemController map;

    private float systemlife; //How much HP the system has
    private float totalPower; //How much energy the system has
    private float totalCooling; //How much cooling the system has
    private bool tutorial; //If tutorial = true, the system HP cannot go down

    private bool[] coolingCellSlotsFree;
    private bool[] powerCellSlotsFree;

    private Cell[] coolingCells;
    private Cell[] powerCells;

    // Use this for initialization
    void Start()
    {
        systemlife = maxLife;
        coolingCellSlotsFree = new bool[coolingCellSlots.Length];
        powerCellSlotsFree = new bool[powerCellSlots.Length];
        coolingCells = new Cell[coolingCellSlots.Length];
        powerCells = new Cell[powerCellSlots.Length];

        for (int i = 0; i < coolingCellSlotsFree.Length; i++)
        {
            coolingCellSlotsFree[i] = true;
            coolingCells[i] = null;
        }
        for (int i = 0; i < powerCellSlotsFree.Length; i++)
        {
            powerCellSlotsFree[i] = true;
            powerCells[i] = null;
        }
        map.UpdateMapSystem(systemlife, maxLife);
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
            map.UpdateMapSystem(systemlife, maxLife);
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
            ConsumeCooling();
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
            ConsumePower();
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

    // should call something in cell so cells know they're attached
    public bool AddCooling(GameObject cell, float amount)
    {
        for (int i = 0; i < coolingCellSlots.Length; i++)
        {
            if (coolingCellSlotsFree[i])
            {
                totalCooling += amount;
                cell.transform.position = coolingCellSlots[i].position;
                coolingCells[i] = cell.GetComponent<Cell>();
                coolingCells[i].Attach(() => { RemoveCoolingCell(i); });
                coolingCells[i].transform.localRotation = Quaternion.identity;
                coolingCellSlotsFree[i] = false;
                return true;
            }
        }
        return false;
    }

    // should call something in cell so cells know they're attached
    public bool AddPower(GameObject cell, float amount)
    {
        for (int i = 0; i < powerCellSlots.Length; i++)
        {
            if (powerCellSlotsFree[i])
            {
                totalPower += amount;
                cell.transform.position = powerCellSlots[i].position;
                powerCells[i] = cell.GetComponent<Cell>();
                powerCells[i].Attach(() => { RemovePowerCell(i); });
                powerCells[i].transform.localRotation = Quaternion.identity;
                powerCellSlotsFree[i] = false;
                return true;
            }
        }
        return false;
    }

    public void RemoveCoolingCell(int i)
    {
        totalCooling -= coolingCells[i].GetCharge();
        coolingCellSlotsFree[i] = true;
        coolingCells[i] = null;
    }

    public void RemovePowerCell(int i)
    {
        totalPower -= powerCells[i].GetCharge();
        powerCellSlotsFree[i] = true;
        powerCells[i] = null;
    }

    private void ConsumeCooling()
    {
        float amount = coolingLossRate * Time.deltaTime;
        for (int i = 0; i < coolingCellSlots.Length && amount > 0f; i++)
        {
            if (coolingCells[i] != null && coolingCells[i].GetCharge() > 0f)
            {
                /* Figure out how much to decrease */
                float dec = Mathf.Min(amount, coolingCells[i].GetCharge());
                coolingCells[i].DecreaseCharge(dec);
                totalCooling -= dec;
                amount -= dec;
            }
        }
    }

    private void ConsumePower()
    {
        float amount = powerLossRate * Time.deltaTime;
        for (int i = 0; i < powerCellSlots.Length && amount > 0f; i++)
        {
            if (powerCells[i] != null && powerCells[i].GetCharge() > 0f)
            {
                /* Figure out how much to decrease */
                float dec = Mathf.Min(amount, powerCells[i].GetCharge());
                powerCells[i].DecreaseCharge(dec);
                totalPower -= dec;
                amount -= dec;
            }
        }
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
        LoadScene.LoadLose();
    }

    void ShowCoreStatus()
    {
        Debug.Log("Core: " + systemlife + " Power: " + totalPower + " Cooling: " + totalCooling);
    }
}