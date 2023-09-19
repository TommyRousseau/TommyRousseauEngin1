using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : CharacterState
{
	float m_defaultTimeInThisState = 0;
	public override void OnEnter()
	{
		
		Debug.Log("Enter state: OnGround\n");
		m_defaultTimeInThisState = m_stateMachine.TimeOnGround;
	}


	public override void OnUpdate()
	{
		m_stateMachine.TimeOnGround -= Time.deltaTime;
	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		Debug.Log("Exit state: OnGround\n");
		m_stateMachine.TimeOnGround = m_defaultTimeInThisState;
	}

	public override bool CanEnter(CharacterState currentState)
	{
		return Input.GetKeyDown(KeyCode.G);
		
	}
	public override bool CanExit()
	{
		return m_stateMachine.TimeOnGround <= 0;
	}
}
