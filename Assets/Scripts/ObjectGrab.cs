using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour 
{
	[SerializeField] Transform m_LookTransform;
	[SerializeField] Transform m_PickupSlot;
	[SerializeField] float m_PickupDistance = 1f;

	Pickup m_Object;
	RaycastHit m_Hit;
	bool m_Holding;

	void Start() 
	{
		m_Holding = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bool DidRaycast = Physics.Raycast(m_LookTransform.position, 
				m_LookTransform.forward, 
				out m_Hit, 
				m_PickupDistance,
				(m_Holding) ? 1 << 9 : 1 << 11);
			if (DidRaycast && m_Holding)
			{
				// I hit a machine
				
			}
			else if (DidRaycast && !m_Holding)
			{
				// I hit a pickup
				m_Object = m_Hit.transform.GetComponent<Pickup>();
				m_Object.Grab(m_PickupSlot);
				m_Holding = true;
			}
			else if (!DidRaycast && m_Holding)
			{
				// Drop the pickup
				m_Object.Drop();
				m_Holding = false;
			}
			else
			{
				// do nothing
			}
		}
	}

}
 