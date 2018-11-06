using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour 
{
	[SerializeField] Transform m_LookTransform;
	[SerializeField] Transform m_PickupSlot;
	[SerializeField] float m_PickupDistance = 1f;

	GameObject m_Object;
	RaycastHit m_Hit;
	bool m_Holding;
	bool m_JustGrabbed;

	void Start() 
	{
		m_Object = null;
		m_Holding = false;
		m_JustGrabbed = false;
	}

	/* Fix this later, I need to handle all the contextual mouse interaction
     * which kind of just gets really messy so I think maybe this script should
	 * handle everything related to the object interaction mode, and then there's
	 * just some other script that handles the switching between the modes by
	 * enabling/disabling different scripts.
	 */
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bool DidRaycast = Physics.Raycast(m_LookTransform.position, 
				m_LookTransform.forward, 
				out m_Hit, 
				m_PickupDistance,
				(m_Holding) ? 1 << 9 : 1 << 11);
			if (DidRaycast && m_Holding && !m_JustGrabbed)
			{
				// I hit a machine
				// do some stuff to update the machine and
				DropAndDestroy();
			}
			else if (DidRaycast && !m_Holding)
			{
				// I hit a pickup
				Grab(m_Hit.transform.gameObject);
			}
			else if (!DidRaycast && m_Holding && !m_JustGrabbed)
			{
				// Drop the pickup
				Debug.Log("DROP");
				Drop();
			}
			if (m_JustGrabbed) { m_JustGrabbed = false; }
		}
	}

	void FixedUpdate()
	{
		if (m_Holding)
		{
			Debug.Log("AAA");
			m_Object.transform.position = Vector3.Lerp(
				m_Object.transform.position,
				m_PickupSlot.transform.position,
				0.5f
			);
		}
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

}
 