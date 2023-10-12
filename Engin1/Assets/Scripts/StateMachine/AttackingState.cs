using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CharacterState
{
	float m_defaultTimeInThisState = 0;

	public override void OnEnter()
	{
		m_stateMachine.Animator.SetTrigger("Attack");
		m_defaultTimeInThisState = m_stateMachine.AttackTime;
	}


	public override void OnUpdate()
	{

	}

	public override void OnFixedUpdate()
	{
		m_defaultTimeInThisState -= Time.deltaTime;
	}


	public override void OnExit()
	{
        m_stateMachine.ToggleAttackHitbox(false);

    }

	public override bool CanEnter(IState currentState)
	{
		if (currentState is FreeState)
		{
			return Input.GetMouseButtonDown(0);
		}
		return false;
		
	}
	public override bool CanExit()
	{
		return m_defaultTimeInThisState <= 0;
	}
}
