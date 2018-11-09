using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently can only charge one cell at a time, probably expand this later

public class ChargeStation : Repairable 
{
	[SerializeField] Transform cellSlot;
	[SerializeField] float chargeSpeed = 1;

	bool charging;
    Cell chargingCell;

	public void InitChargeStation()
	{
		base.InitRepairable();
		charging = false;
	}

	public bool InsertCell(GameObject cell)
    {
        if (!charging)
        {
            chargingCell = cell.GetComponent<Cell>();
            chargingCell.Attach(() => {RemoveCell();});
            cell.transform.parent = cellSlot;
            cell.transform.localPosition = Vector3.zero;
            cell.transform.localRotation = Quaternion.identity;
            charging = true;
            return true;
        }
        return false;
    }

    public void RemoveCell()
    {
        if (charging)
        {
		    charging = false;
            chargingCell = null;
        }
    }

    public void Charge()
    {
        if (charging)
        {
            chargingCell.IncreaseCharge(chargeSpeed * Time.deltaTime);
        }    
    }	
}
