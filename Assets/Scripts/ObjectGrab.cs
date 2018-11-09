using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour 
{
	[SerializeField] Transform m_LookTransform;
	[SerializeField] Transform m_PickupSlot;
	[SerializeField] float m_PickupDistance = 1f;

	public static ObjectGrab Player;

	int m_MachineLayer = 1 << 9;
	int m_PickupLayer = 1 << 11;

	GameObject m_Object;
	RaycastHit m_MachineHit, m_PickupHit;
	RaycastHit m_Hit;
	bool m_Holding;
	bool m_JustGrabbed;

	void Start() 
	{
		m_Object = null;
		m_Holding = false;
		m_JustGrabbed = false;
		Player = this;
	}

	void Update()
	{
		// Need to check if I hit a machine and also if I hit a thing
		if (Input.GetMouseButtonDown(0))
		{
			// use two rays, a pickup ray and a machine ray
			bool machineCast = RaycastOnLayer(m_MachineLayer, out m_MachineHit);
			bool pickupCast = RaycastOnLayer(m_PickupLayer, out m_PickupHit);
			
			/* If I'm looking at a machine and holding an object */
			if (machineCast && m_Holding)
			{
				/* Use Object with machine */
				UseObjectWithMachine();
			}
			/* If I'm looking at a machine and not holding anything */
			// else if (machineCast && !m_Holding)
			// {
			// 	/* Try to get object from machine */
			// 	TryToGetCellFromMachine();
			// }
			/* If I'm looking at a pickup and not holding anything */
			if (pickupCast && !m_Holding)
			{
				/* Grab item */
				Grab(m_PickupHit.transform.gameObject);
			}
			/* If I'm looking at a pickup and not holding anything */
			if (m_Holding && !m_JustGrabbed)
			{
				/* Drop Item */
				Drop();
			}

			// Toggle so you can't drop
			if (m_JustGrabbed) { m_JustGrabbed = false; }

		}
	}

	void UseObjectWithMachine()
	{
		// I hit a machine
		// do some stuff to update the machine and
		bool shouldAttachObject = false;
		switch (m_MachineHit.transform.tag)
		{
			case "Core":
				CoreScript core = GetHitComponent<CoreScript>(m_MachineHit);
				if (IsHoldingWithTag("PowerCell"))
				{
					shouldAttachObject = core.AddPower(m_Object, GetHeldCellCharge());
				}
				else if (IsHoldingWithTag("CoolingCell"))
				{
					shouldAttachObject = core.AddCooling(m_Object, GetHeldCellCharge());
				}
				break;
			case "Filter":
				FilterScript filter = GetHitComponent<FilterScript>(m_MachineHit);
				if (IsHoldingWithTag("FilterPickup"))
				{
					shouldAttachObject = filter.InsertFilter(m_Object);
				}
				break;
			case "Refiner":
				RefinerScript refiner = GetHitComponent<RefinerScript>(m_MachineHit);
				if (IsHoldingWithTag("PowerCell"))
				{
					shouldAttachObject = refiner.InsertCell(m_Object);
				}
				break;
			case "Radiator":
				RadiatorScript radiator = GetHitComponent<RadiatorScript>(m_MachineHit);
				if (IsHoldingWithTag("CoolingCell"))
				{
					shouldAttachObject = radiator.InsertCell(m_Object);
				}
				break;
		}
		// should only destroy if the interaction makes sense
		if (shouldAttachObject) AttachObject();
	}

	void TryToGetCellFromMachine()
	{
		switch (m_MachineHit.transform.tag)
		{
			case "Filter":
				// removes the filter
				GetHitComponent<FilterScript>(m_MachineHit).RemoveFilter();
				break;
			case "Radiator":
				GetHitComponent<RadiatorScript>(m_MachineHit).RemoveCell();
				break;
			case "Refiner":
				GetHitComponent<RefinerScript>(m_MachineHit).RemoveCell();
				break;
		}
	}

	T GetHitComponent<T>(RaycastHit hit)
	{
		return hit.transform.gameObject.GetComponent<T>();
	}

	float GetHeldCellCharge()
	{
		if (m_Holding)
		{
			return m_Object.GetComponent<Cell>().GetCharge();
		}
		return -1f;
	}

	bool RaycastOnLayer(int layer, out RaycastHit hit)
	{
		return Physics.Raycast(m_LookTransform.position, 
			m_LookTransform.forward, 
			out hit, 
			m_PickupDistance,
			layer
		);
	}

	public void Grab(GameObject pickup)
	{
		m_Object = pickup;
		Cell cell = m_Object.GetComponent<Cell>();
		if (cell.IsAttached())
		{
			cell.Detach();
		}
		m_Holding = true;
		Rigidbody rb = m_Object.GetComponent<Rigidbody>();
		if (rb != null) rb.isKinematic = true;
		m_Object.transform.parent = m_PickupSlot.transform;
		m_Object.transform.localPosition = Vector3.zero;
		m_JustGrabbed = true;
	}

	public void Drop()
	{
		m_Object.GetComponent<Rigidbody>().isKinematic = false;
		m_Object.transform.parent = null;
		m_Object = null;
		m_Holding = false;
	}

	public void AttachObject()
	{
		m_Object.transform.parent = null;
		m_Object = null;
		m_Holding = false;
	}

	public bool IsHolding() { return m_Holding; }

	public bool IsHoldingWithTag (string tag)
	{
		if (m_Holding)
		{
			return (m_Object.tag == tag);
		}
		return false;
	}

}
 