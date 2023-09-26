using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : CharacterState
{

	public override void OnEnter()
	{
		m_stateMachine.JumpHeight = m_stateMachine.transform.position.y;
	}


	public override void OnUpdate()
	{

	}

	public override void OnFixedUpdate()
	{
		Vector3 currentDir = Vector3.zero;
		Vector3 speedVector = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
		{

			currentDir += m_stateMachine.Camera.transform.forward;

			speedVector.z += 1;

		}

		if (Input.GetKey(KeyCode.S))
		{
			currentDir += -m_stateMachine.Camera.transform.forward;
			speedVector.z -= 1;

		}

		if (Input.GetKey(KeyCode.D))
		{
			currentDir += m_stateMachine.Camera.transform.right;
			speedVector.x += 1;


		}

		if (Input.GetKey(KeyCode.A))
		{
			currentDir += -m_stateMachine.Camera.transform.right;
			speedVector.x -= 1;

		}

		//For controllers
		//speedVector.z = (Input.GetAxis("Horizontal"));
		//speedVector.x = (Input.GetAxis("Vertical"));



		speedVector.Normalize();

		//Set max speed for each direction
		speedVector.x = speedVector.x * m_stateMachine.SideMaxVelocity;
		if (speedVector.z > 0)
		{
			speedVector.z = speedVector.z * m_stateMachine.ForwardMaxVelocity;
		}
		else if (speedVector.z < 0)
		{
			speedVector.z = speedVector.z * m_stateMachine.BackMaxVelocity;
		}


		currentDir = Vector3.ProjectOnPlane(currentDir, Vector3.up);
		currentDir.Normalize();

		float maxSpeed = Mathf.Sqrt((speedVector.x * speedVector.x) + (speedVector.z * speedVector.z));


		if (m_stateMachine.Rb.velocity.magnitude < maxSpeed)
		{
			m_stateMachine.Rb.AddForce(currentDir * m_stateMachine.AirAccelerationValue, ForceMode.Acceleration);
		}

		if (maxSpeed == 0 && m_stateMachine.Rb.velocity.magnitude > 1)
		{
			m_stateMachine.Rb.AddForce(-m_stateMachine.Rb.velocity * m_stateMachine.StopSpeed);

		}
	}


	public override void OnExit()
	{

	}

	public override bool CanEnter(CharacterState currentState)
	{
		if (m_stateMachine.IsInContactWithFloor() == false)
		{
			if (currentState is JumpState)
			{
				return true;
			}
			if (currentState is FreeState)
			{
				return true;
			}
		}
		
		return false;

	}
	public override bool CanExit()
	{
		return true;
	}

}
