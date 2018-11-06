using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour 
{
	[SerializeField] GameObject m_Pickup;
	[SerializeField] float m_InteractDistance;

	ObjectGrab m_PlayerGrab;
	Transform m_Player;

	void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		m_PlayerGrab = player.GetComponent<ObjectGrab>();
		m_Player = player.transform.GetChild(0);
	}

	void OnMouseDown()
	{
		if (Vector3.Distance(m_Player.position, transform.position) <= m_InteractDistance)
		{
			GameObject p = Instantiate(m_Pickup, transform.position + (0.7f * Vector3.up), Quaternion.identity);
			m_PlayerGrab.Grab(p);
		}
	}

	[ContextMenu("Spawn Pickup")]
	void DoSpawnPickup()
	{
		Debug.Log("SPAWN");
	}
	
}
