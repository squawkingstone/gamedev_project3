using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairController : MonoBehaviour 
{
	[SerializeField] float m_RepairSpeed;
	[SerializeField] float m_InteractDistance;

	TestRepairableSystem m_RepairableSystem;
	Transform m_LookTransform;
	RaycastHit m_Hit;
	bool m_Repairing;

	void Start()
	{
		m_LookTransform = transform.GetChild(0);
		m_Repairing = false;
	}

	/* Handle the repair behavior */
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && 
			Physics.Raycast(
				m_LookTransform.position,
				m_LookTransform.forward,
				out m_Hit,
				m_InteractDistance,
				1 << 9))
		{
			// Start repairing
			m_RepairableSystem = m_Hit.transform.GetComponent<TestRepairableSystem>();
			m_Repairing = true;
		}
		else if (Input.GetMouseButton(0) && m_Repairing)
		{
			// continue repairing
			m_RepairableSystem.Repair(m_RepairSpeed * Time.deltaTime);
		}
		else if (Input.GetMouseButtonUp(0) && m_Repairing)
		{
			m_RepairableSystem = null;
			m_Repairing = false;
		}
	}

}
