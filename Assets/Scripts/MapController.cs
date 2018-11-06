using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MapController : MonoBehaviour 
{
	[SerializeField] GameObject m_Map;
	FirstPersonController m_FPS;
	CharacterController m_Character;
	MapMovement m_Movement;

	void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		m_FPS = player.GetComponent<FirstPersonController>();
		m_Character = player.GetComponent<CharacterController>();
		m_Movement = GetComponent<MapMovement>();
		m_Movement.enabled = false;
		m_Map.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			m_FPS.enabled = m_Movement.enabled;
			m_Character.enabled = m_Movement.enabled;
			m_Movement.enabled = !m_Movement.enabled;
			m_Map.SetActive(m_Movement.enabled);
		}
	}

}
