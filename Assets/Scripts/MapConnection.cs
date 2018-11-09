using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConnection : MonoBehaviour 
{
	LineRenderer m_Line;
	Transform m_End;
	// Renderer m_Renderer;
	// Material m_Material;

	void Awake()
	{
		m_Line = GetComponent<LineRenderer>();
		// m_Renderer = GetComponent<Renderer>();
		m_End = transform.GetChild(0);
	}

	void Start()
	{
		// m_Renderer.sharedMaterial = new Material(m_Renderer.sharedMaterial);
		// m_Material = m_Renderer.sharedMaterial;
		m_Line.SetPosition(0, transform.position);
		m_Line.SetPosition(1, m_End.position);
		m_Line.enabled = false;
	}
	
	public void EnableLine()
	{
		Debug.Log("Line Enabled");
		m_Line.enabled = true;
	}

	public void DisableLine()
	{
		m_Line.enabled = false;
	}

	public bool IsEnable()
	{
		return m_Line.enabled;
	}

}
