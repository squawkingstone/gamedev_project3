using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour 
{
	[SerializeField] float m_RotationSpeed;
	[SerializeField] float m_MaxHeight;
	[SerializeField] float m_MinHeight;
	
	float x,y;

	void Update () 
	{
		x = Input.GetAxis("Mouse X");
		y = Input.GetAxis("Mouse Y");

		transform.localRotation *= Quaternion.AngleAxis(x * m_RotationSpeed, Vector3.up);
		y = Mathf.Clamp(transform.position.y + y, m_MinHeight, m_MaxHeight);
		transform.position = new Vector3(0f, y, 0f);
	}
}
