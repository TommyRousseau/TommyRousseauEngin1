using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CharacterState
{
	public override void OnEnter()
	{
		Debug.Log("Enter state: Attacking\n");
	}


	public override void OnUpdate()
	{

	}

	public override void OnFixedUpdate()
	{

	}


	public override void OnExit()
	{
		Debug.Log("Exit state: Attacking\n");
	}

	public override bool CanEnter(CharacterState currentState)
	{

		return Input.GetMouseButtonDown(0);
	}
	public override bool CanExit()
	{
		return true;
	}
}
