using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSelector : MonoBehaviour 
{
	[SerializeField] Transform m_Look;
	[SerializeField] float m_LookDistance;

	RaycastHit m_Hit;

	void Update()
	{
		if (Physics.Raycast(m_Look.position, m_Look.forward, out m_Hit, m_LookDistance, 1 << 9))
		{
			m_Hit.transform.GetComponent<Highlightable>();
		}
	}	
}
