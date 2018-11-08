using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently can only charge one cell at a time, probably expand this later

public class ChargeStation : Repairable 
{
	[SerializeField] GameObject cellPrefab;
	[SerializeField] Transform spawnLocation;
	[SerializeField] float maxCharge = 100;
	[SerializeField] float chargeSpeed = 1;

	bool charging;
	float chargeAmount;

	public void InitChargeStation()
	{
		base.InitRepairable();
		charging = false;
	}

	public void InsertCell(float amount)
    {
        if (!charging)
        {
            charging = true;
            chargeAmount = amount;
        }
    }

    public void RemoveCell()
    {
        // instantiates a cell charged by a certain amount;
		Cell.CreateAndGrab(cellPrefab, spawnLocation.position, chargeAmount);
		charging = false;
    }

    public void Charge()
    {
        if (chargeAmount < maxCharge && charging) chargeAmount = Mathf.Clamp(chargeAmount + (chargeSpeed * Time.deltaTime), 0f, maxCharge); 
    }	
}
