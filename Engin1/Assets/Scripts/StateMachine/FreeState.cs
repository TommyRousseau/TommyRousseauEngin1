using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeState : CharacterState
{
	private CharacterControllerStateMachine m_stateMachine;

	void override void OnEnter()
	{

	}


	void override void OnUpdate()
	{
		
	}

	public override void OnFixedUpdate()
	{
		//m_currentState.OnUpdate();

		var vectorOnFloor = Vector3.ProjectOnPlane(Camera.transform.forward, Vector3.up);
		vectorOnFloor.Normalize();


		if (Input.GetKey(KeyCode.W))
		{
			Rb.AddForce(vectorOnFloor * AccelerationValue, ForceMode.Acceleration);
		}

		if (Rb.velocity.magnitude > MaxVelocity)
		{
			Rb.velocity = Rb.velocity.normalized;
			Rb.velocity *= MaxVelocity;
		}

		// print(Rb.velocity.magnitude);
	}



	void IState.OnExit()
	{

	}

}
