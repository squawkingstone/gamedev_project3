using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
	bool m_Grabbed;
	Transform m_Target;
	Rigidbody m_Rigidbody;

// all pickups get spawned in grabbed, so they'll try to hop into the pickup
// slot but if they can't then they'll destroy themselves, this seems l

	void Awake()
	{
		m_Grabbed = false;
		m_Target = null;
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	void Start()
	{
		//TryToGetGrabbed();
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

	void TryToGetGrabbed()
	{
		ObjectGrab grab = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectGrab>();
		if (grab.IsHolding())
		{
			Destroy(this.gameObject);
		}
		else
		{
			grab.Grab(this.gameObject);
		}
	}
	
}
