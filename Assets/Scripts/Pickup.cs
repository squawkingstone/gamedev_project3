using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
	bool m_Grabbed;
	Transform m_Target;
	Rigidbody m_Rigidbody;

	void Awake()
	{
		m_Grabbed = false;
		m_Target = null;
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (m_Grabbed)
		{
			transform.position = Vector3.Lerp(transform.position, m_Target.position, 0.5f);
		}
	}

	public void Grab(Transform t)
	{
		m_Target = t;
		m_Rigidbody.isKinematic = true;
		m_Grabbed = true;
	}

	public void Drop()
	{
		m_Grabbed = false;
		m_Rigidbody.isKinematic = false;
		m_Target = null;
	}
	
}
