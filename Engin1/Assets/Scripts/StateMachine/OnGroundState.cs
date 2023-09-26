using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundState : CharacterState
{
	float m_defaultTimeInThisState = 0;
	public override void OnEnter()
	{
		//Reset high fall check
		m_stateMachine.IsFallStuned = false;

		m_stateMachine.Animator.SetTrigger("Stun");
		m_defaultTimeInThisState = m_stateMachine.TimeOnGround;

	}


	public override void OnUpdate()
	{
		m_defaultTimeInThisState -= Time.deltaTime;
	
	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		
	}

	public override bool CanEnter(CharacterState currentState)
	{
		return m_stateMachine.IsFallStuned;
		
	}
	public override bool CanExit()
	{
		return m_defaultTimeInThisState <= 0;
	}
}
