using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionState : GameState
{
	float m_defaultTimeInThisState = 0;
	public override void OnEnter()
	{


	}


	public override void OnUpdate()
	{
	
	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		
	}

	public override bool CanEnter(IState currentState)
	{
		return true;

	}
	public override bool CanExit()
	{
		return true;
	}
}
