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

	/* Fix this later, I need to handle all the contextual mouse interaction
     * which kind of just gets really messy so I think maybe this script should
	 * handle everything related to the object interaction mode, and then there's
	 * just some other script that handles the switching between the modes by
	 * enabling/disabling different scripts.
	 */
	void Update()
	{
		// Need to check if I hit a machine and also if I hit a thing
		if (Input.GetMouseButtonDown(0))
		{
			// use two rays, a pickup ray and a machine ray
			
			bool DidRaycast = RaycastOnLayer((m_Holding) ? m_MachineLayer : m_PickupLayer, out m_Hit);
			if (DidRaycast && m_Holding && !m_JustGrabbed)
			{
				UseObjectWithMachine();	
			}
			else if (DidRaycast && !m_Holding)
			{
				// I hit a pickup
				Grab(m_Hit.transform.gameObject);
			}
			else if (!DidRaycast && m_Holding && !m_JustGrabbed)
			{
				// Drop the pickup
				Drop();
			}
			if (m_JustGrabbed) { m_JustGrabbed = false; }
		}
	}

	void FixedUpdate()
	{
		if (m_Holding)
		{
			m_Object.transform.position = Vector3.Lerp(
				m_Object.transform.position,
				m_PickupSlot.transform.position,
				0.5f
			);
		}
	}

	void UseObjectWithMachine()
	{
		// I hit a machine
		// do some stuff to update the machine and
		switch (m_Hit.transform.tag)
		{
			case "Core":
				CoreScript core = GetHitComponent<CoreScript>();
				if (IsHoldingWithTag("PowerCell"))
				{
					core.AddPower(GetHeldCellCharge());
				}
				else if (IsHoldingWithTag("CoolingCell"))
				{
					core.AddCooling(GetHeldCellCharge());
				}
				break;
			case "Filter":
				FilterScript filter = GetHitComponent<FilterScript>();
				if (IsHoldingWithTag("FilterPickup"))
				{
					filter.InsertFilter(GetHeldCellCharge());
				}
				break;
			case "Refiner":
				RefinerScript refiner = GetHitComponent<RefinerScript>();
				if (IsHoldingWithTag("PowerCell"))
				{
					refiner.InsertCell(GetHeldCellCharge());
				}
				break;
			case "Radiator":
				RadiatorScript radiator = GetHitComponent<RadiatorScript>();
				if (IsHoldingWithTag("CoolingCell"))
				{
					radiator.InsertCell(GetHeldCellCharge());
				}
				break;
		}
		DropAndDestroy();
	}

	T GetHitComponent<T>()
	{
		return m_Hit.transform.gameObject.GetComponent<T>();
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
		m_Holding = true;
		Rigidbody rb = m_Object.GetComponent<Rigidbody>();
		if (rb != null) rb.isKinematic = true;
		m_JustGrabbed = true;
	}

	public void Drop()
	{
		m_Object.GetComponent<Rigidbody>().isKinematic = false;
		m_Object = null;
		m_Holding = false;
	}

	public void DropAndDestroy()
	{
		Destroy(m_Object.gameObject);
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
 