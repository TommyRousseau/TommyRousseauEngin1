using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeState : CharacterState
{

    public override void OnEnter()
	{
		Debug.Log("Enter state: FreeState\n");
	}


    public override void OnUpdate()
	{
		
	}

	public override void OnFixedUpdate()
	{
		//m_currentState.OnUpdate();

		var vectorOnFloor = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
		vectorOnFloor.Normalize();


		if (Input.GetKey(KeyCode.W))
		{
            m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
		}

		if (m_stateMachine.Rb.velocity.magnitude > m_stateMachine.MaxVelocity)
		{
            m_stateMachine.Rb.velocity = m_stateMachine.Rb.velocity.normalized;
            m_stateMachine.Rb.velocity *= m_stateMachine.MaxVelocity;
		}

		// print(Rb.velocity.magnitude);
	}

	public override void OnExit()
	{
        Debug.Log("Exit state: FreeState\n");
    }

    public override bool CanEnter()
    {
		//Can enter only if touching ground
		return true;	
    }
    public override bool CanExit()
    {
		return true;
    }
}
