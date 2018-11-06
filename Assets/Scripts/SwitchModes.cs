using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModes : MonoBehaviour 
{
	private enum Mode
	{
		REPAIR_MODE,
		OBJECT_MODE
	}

	RepairController m_Repair;
	ObjectGrab m_Object;

	Mode m_Mode;

	void Awake()
	{
		m_Repair = GetComponent<RepairController>();
		m_Object = GetComponent<ObjectGrab>();
	}

	void Start()
	{
		SetMode(Mode.REPAIR_MODE);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SetMode((m_Mode == Mode.OBJECT_MODE) ? Mode.REPAIR_MODE : Mode.OBJECT_MODE);
		}
	}

	void SetMode(Mode m)
	{
		m_Mode = m;
		switch (m_Mode)
		{
			case Mode.REPAIR_MODE:
				m_Repair.enabled = true;
				m_Object.enabled = false;
				break;
			case Mode.OBJECT_MODE:
				m_Repair.enabled = false;
				m_Object.enabled = true;
				break;
		}
	}
}
