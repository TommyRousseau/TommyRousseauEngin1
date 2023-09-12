using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingUpState : CharacterState
{
	float m_defaultTimeInThisState = 0;
	public override void OnEnter()
	{
		Debug.Log("Enter state: GettingUP\n");
		m_defaultTimeInThisState = m_stateMachine.TimeGettingUp;
	}


	public override void OnUpdate()
	{
		m_stateMachine.TimeGettingUp -= Time.deltaTime;
	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		Debug.Log("Exit state: GettingUP\n");
		m_stateMachine.TimeGettingUp = m_defaultTimeInThisState;
	}

	public override bool CanEnter()
	{
		return m_stateMachine.TimeOnGround <= 0;
	}
	public override bool CanExit()
	{
		return m_stateMachine.TimeGettingUp < 0;
	}
}
