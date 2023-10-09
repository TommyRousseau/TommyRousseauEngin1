using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : IState
{
	protected GameManager m_stateMachine;


	public void OnStart(GameManager stateMachine)
	{
		m_stateMachine = stateMachine;
	}

	public virtual void OnEnter()
	{
		
	}

	public virtual void OnExit()
	{

	}

	public virtual void OnFixedUpdate()
	{

	}

	public virtual void OnUpdate()
	{

	}

	public virtual bool CanEnter(CharacterState currentState)
	{
		return true;
	}

	public virtual bool CanExit()
	{
		return true;
	}
}
