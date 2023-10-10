using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingUpState : CharacterState
{
	float m_defaultTimeInThisState = 0;
	public override void OnEnter()
	{
		m_stateMachine.Animator.SetTrigger("GettingUp");
		m_defaultTimeInThisState = m_stateMachine.TimeGettingUp;
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

	public override bool CanEnter(IState currentState)
	{
		if (currentState is OnGroundState)
		{
			return true;
		}
		return false;
		
	}
	public override bool CanExit()
	{
		return m_defaultTimeInThisState < 0;
	}
}
