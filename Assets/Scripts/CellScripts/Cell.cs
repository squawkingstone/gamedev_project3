﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour 
{
	[SerializeField] float maxCharge;
	[SerializeField] Renderer chargeRenderer;
	[SerializeField] bool hasChargeMaterial;
	[SerializeField] AudioSource clink;

	float charge;
	Material chargeMat;
	bool attached;
	UnityAction removeAction;

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

	public void IncreaseCharge(float amount)
	{
		charge = Mathf.Clamp(charge + amount, 0f, maxCharge);
		UpdateMaterial();
	}

	public void DecreaseCharge(float amount)
	{
		IncreaseCharge(-amount);
	}
	
	void SetCharge(float charge)
	{
		this.charge = charge;
		UpdateMaterial();
	}

	void UpdateMaterial()
	{
		if (chargeMat != null)
		{
			// do the material update stuff
			chargeMat.SetFloat("_Threshold", charge/maxCharge);
		}
	}

	public float GetCharge()
	{
		return charge;
	}

	public void Attach(UnityAction removeAction)
	{
		Debug.Log("Attached");
		attached = true;
		this.removeAction = removeAction;
		PlayClink();
	}

	public void Detach()
	{
		Debug.Log("Detach");
		attached = false;
		this.removeAction();
		PlayClink();
	}

	public bool IsAttached()
	{
		return attached;
	}

	public void PlayClink()
	{
		clink.Play();
	}

    void OnCollisionEnter(Collision collision)
	{
		PlayClink();
	}
}
