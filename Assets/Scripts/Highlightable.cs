using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour 
{
	[SerializeField] Color m_HighlightColor = Color.yellow;
	[SerializeField] float m_HighlightDistance = 5f;

	Transform m_LookTransform;
	Material m_Material;
	Color m_DefaultColor;
	bool m_Highlighted;

	void Start ()
	{
		m_LookTransform = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
		Renderer render = GetComponentInChildren<Renderer>();
		render.sharedMaterial = new Material(render.sharedMaterial);
		m_Material = render.sharedMaterial;
		m_DefaultColor = m_Material.color;
		m_Highlighted = false;
	}

	void OnMouseEnter () 
	{ 
		if (!m_Highlighted && Vector3.Distance(m_LookTransform.position, transform.position) <= m_HighlightDistance)
		{
			Highlight(); 
		}
	}
	void OnMouseExit () { if (m_Highlighted) Unhighlight(); }

	public void Highlight ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		m_Material.color = m_HighlightColor;
		m_Highlighted = true;
	}

	public void Unhighlight ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		m_Material.color = m_DefaultColor;
		m_Highlighted = false;
	}

}
