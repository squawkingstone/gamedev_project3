using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConnection : MonoBehaviour 
{
	LineRenderer m_Line;
	Transform m_End;
	Renderer m_Renderer;
	Material m_Material;

	void Awake()
	{
		m_Line = GetComponent<LineRenderer>();
		m_Renderer = GetComponent<Renderer>();
		m_End = transform.GetChild(0);
	}

	void Start()
	{
		m_Renderer.sharedMaterial = new Material(m_Renderer.sharedMaterial);
		m_Material = m_Renderer.sharedMaterial;
		m_Line.SetPosition(0, transform.position);
		m_Line.SetPosition(1, m_End.position);
		m_Line.enabled = false;
	}
	
	public void EnableLine(Color color)
	{
		m_Line.enabled = true;
		SetColor(color);
	}

	public void DisableLine()
	{
		m_Line.enabled = false;
	}

	public void SetColor(Color color)
	{
		m_Material.SetColor("_Color", color);
	}

	[ContextMenu("Enable Line")]
	public void EnableLineWithGreen()
	{
		EnableLine(Color.green);
	}

}
