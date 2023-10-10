using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : IState
{
	protected CharacterControllerStateMachine m_stateMachine;


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

    public virtual bool CanExit()
    {
		return true;
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnStart(CharacterControllerStateMachine stateMachineRef)
    {
		m_stateMachine = stateMachineRef;
    }

    public virtual bool CanEnter(IState currentState)
    {
        throw new System.NotImplementedException();
    }
}
