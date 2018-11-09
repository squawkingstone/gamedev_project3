using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour 
{
	[SerializeField] GameObject m_Pickup;
	[SerializeField] float m_DefaultCharge;
	[SerializeField] float m_InteractDistance;

	Transform m_Player;

	void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		// m_PlayerGrab = player.GetComponent<ObjectGrab>();
		m_Player = player.transform.GetChild(0);
	}

	void OnMouseDown()
	{
		Debug.Log("EYAH");
		if (Vector3.Distance(m_Player.position, transform.position) <= m_InteractDistance 
			&& !ObjectGrab.Player.IsHolding())
		{
			Debug.Log("hello");
			Cell.CreateAndGrab(m_Pickup, transform.position + (Vector3.up), m_DefaultCharge);
		}
	}
	
}
