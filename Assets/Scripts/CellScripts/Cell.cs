using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour 
{
	[SerializeField] float maxCharge;
	[SerializeField] Renderer chargeRenderer;
	[SerializeField] bool hasChargeMaterial;

	float charge;
	Material chargeMat;

	public static GameObject CreateCell(GameObject prefab, Vector3 position, float startingCharge)
	{
		GameObject c = Instantiate(prefab, position, Quaternion.identity);
		c.GetComponent<Cell>().InitCell(startingCharge); 
		return c; 
	}

	public static void CreateAndGrab(GameObject prefab, Vector3 position, float startingCharge)
	{
		GameObject c = CreateCell(prefab, position, startingCharge);
		ObjectGrab.Player.Grab(c);
	}

	public void InitCell(float charge)
	{
		if (hasChargeMaterial)
		{
			chargeRenderer.sharedMaterial = new Material(chargeRenderer.sharedMaterial);
			chargeMat = chargeRenderer.sharedMaterial;
		}
		else 
		{
			chargeMat = null;
		}
		SetCharge(charge);
	}
	
	void SetCharge(float charge)
	{
		this.charge = charge;
		UpdateMaterial();
		Debug.Log(this.charge);
	}

	void UpdateMaterial()
	{
		if (chargeMat != null)
		{
			// do the material update stuff
		}
	}

	public float GetCharge()
	{
		return charge;
	}
}
