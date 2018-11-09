using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystemController : MonoBehaviour 
{
	[SerializeField] Material m_DefaultMaterial;
	[SerializeField] Color m_Healthy;
	[SerializeField] Color m_Damaged;

	[SerializeField] Renderer[] m_SubRenderers;
	Material m_Material;

	// [SerializeField] MapConnection[] connections;

	void Awake () 
	{
		// get all the materials
		m_SubRenderers[0].sharedMaterial = new Material(m_DefaultMaterial);
		m_Material = m_SubRenderers[0].sharedMaterial;
		for (int i = 0; i < m_SubRenderers.Length; i++)
		{
			m_SubRenderers[i].sharedMaterial = m_Material;
		}
	}
	
	public void UpdateMapSystem(float systemLife, float maxLife)
	{
		
		m_Material.SetColor(
			"_Color", 
			Color.Lerp(m_Damaged, m_Healthy, Mathf.Clamp01(systemLife / maxLife))
		);
		// Debug.Log(enable);
		// for (int i = 0; i < connections.Length; i++)
		// {
		// 	Debug.Log("AAAA");
		// 	if (enable)
		// 	{
		// 		connections[i].EnableLine();
		// 	}
		// 	else
		// 	{
		// 		connections[i].DisableLine();
		// 	}
		// }
	}

}
