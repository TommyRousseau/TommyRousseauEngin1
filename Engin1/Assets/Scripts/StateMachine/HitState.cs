using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CharacterState
{

	public override void OnEnter()
	{
		Debug.Log("Enter state: HitState\n");
	}


	public override void OnUpdate()
	{

	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		Debug.Log("Exit state: HitState\n");
	}

	public override bool CanEnter(CharacterState currentState)
	{

		return Input.GetKeyDown(KeyCode.H);
	}
	public override bool CanExit()
	{
		return true;
	}

}
