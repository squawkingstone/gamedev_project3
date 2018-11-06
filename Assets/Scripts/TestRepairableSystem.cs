using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRepairableSystem : MonoBehaviour 
{
	[SerializeField] float m_MaxHealth;
	[SerializeField] float m_DecayRate;
	[SerializeField] Color m_HealthyColor;
	[SerializeField] Color m_DeadColor;

	[SerializeField] Renderer m_Renderer;
	
	Material m_Material;
	float m_Health;
	bool m_Decaying;

	float hHue, hSat, hVal;
	float dHue, dSat, dVal;
	float blend;

	void Start()
	{
		m_Renderer.sharedMaterial = new Material(m_Renderer.sharedMaterial);
		m_Material = m_Renderer.sharedMaterial;
		m_Health = m_MaxHealth;
		Color.RGBToHSV(m_HealthyColor, out hHue, out hSat, out hVal);
		Color.RGBToHSV(m_DeadColor,    out dHue, out dSat, out dVal);
	}

	void Update()
	{
		if (m_Decaying)
		{
			DecayHealth();
		}
		UpdateColor();
	}

	public void Repair(float amount)
	{
		m_Health = ClampInc(m_Health, amount, 0f, m_MaxHealth);
	}

	float ClampInc(float value, float increment, float min, float max)
	{
		return Mathf.Clamp(value + increment, min, max);
	}

	void DecayHealth()
	{
		m_Health = ClampInc(m_Health, -(m_DecayRate * Time.deltaTime), 0f, m_MaxHealth);
	}

	void UpdateColor()
	{
		blend = Mathf.Clamp01(m_Health / m_MaxHealth);
		m_Material.color = Color.HSVToRGB(
			Mathf.Lerp(dHue, hHue, blend),
			Mathf.Lerp(dSat, hSat, blend),
			Mathf.Lerp(dVal, hVal, blend)
		);
	}

	/* TESTING FUNCTIONS */
	[ContextMenu("Enable Decay")]
	void EnableDecay() { m_Decaying = true; }
	[ContextMenu("Disable Decay")]
	void DisableDecay() { m_Decaying = false; }
	[ContextMenu("Set Health to 0%")]
	void SetHealth000() { SetPercentageHealth(0f); }
	[ContextMenu("Set Health to 25%")]
	void SetHealth025() { SetPercentageHealth(0.25f); }
	[ContextMenu("Set Health to 50%")]
	void SetHealth050() { SetPercentageHealth(0.5f); }
	[ContextMenu("Set Health to 75%")]
	void SetHealth075() { SetPercentageHealth(0.75f); }
	[ContextMenu("Set Health to 100%")]
	void SetHealth100() { SetPercentageHealth(1f); }


	void SetPercentageHealth(float percentage)
	{
		m_Health = m_MaxHealth * Mathf.Clamp01(percentage);
	}

}
