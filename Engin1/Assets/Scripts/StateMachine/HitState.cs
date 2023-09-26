using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CharacterState
{
	
	private float m_currentStateTimer = 0.0f;

	public override void OnEnter()
	{
		m_stateMachine.Animator.SetTrigger("Hit");
		m_currentStateTimer = m_stateMachine.HitTime;

	}


	public override void OnUpdate()
	{
		if(m_stateMachine.IsInContactWithFloor())
		{
			m_currentStateTimer -= Time.deltaTime;
		}
		
	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		
	}

	public override bool CanEnter(CharacterState currentState)
	{
		if (currentState is FreeState)
		{
			return m_stateMachine.IsGettingHit();
		}

		if (currentState is JumpState)
		{
			return m_stateMachine.IsGettingHit();
		}

		if (currentState is FallingState)
		{
			return m_stateMachine.IsGettingHit();
		}

		if (currentState is AttackingState)
		{
			return m_stateMachine.IsGettingHit();
		}


		return false;
	}
	public override bool CanExit()
	{
		return m_currentStateTimer <= 0;
	
	}

}
