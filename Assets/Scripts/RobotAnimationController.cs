using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour 
{
	[SerializeField] Animator animator;
	[SerializeField] CharacterController controller;

	void Update()
	{
		bool moving = (Vector3.Distance(controller.velocity, Vector3.zero) > 0.001f);
		animator.SetBool("Moving", moving);
	}

}
