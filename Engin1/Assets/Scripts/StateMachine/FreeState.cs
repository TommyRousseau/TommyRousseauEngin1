using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeState : CharacterState
{
	Vector3 vectorOnFloor = new Vector3();

	public override void OnEnter()
	{
		Debug.Log("Enter state: FreeState\n");
	}


    public override void OnUpdate()
	{
		
	}

	public override void OnFixedUpdate()
	{
		
		if (Input.GetKey(KeyCode.W))
		{
		
            vectorOnFloor = GetVectorOnFloor(m_stateMachine.Camera.transform.forward);
			m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.ForwardAccelerationValue, ForceMode.Acceleration);
		}
		if (Input.GetKey(KeyCode.S))
		{
			vectorOnFloor = GetVectorOnFloor(-m_stateMachine.Camera.transform.forward);
			m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.ForwardAccelerationValue, ForceMode.Acceleration);
		}

		if (Input.GetKey(KeyCode.D))
		{
			vectorOnFloor = GetVectorOnFloor(m_stateMachine.Camera.transform.right);
			m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.SideAccelerationValue, ForceMode.Acceleration);
		}

		if (Input.GetKey(KeyCode.A))
		{
			vectorOnFloor = GetVectorOnFloor(-m_stateMachine.Camera.transform.right);
			m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.SideAccelerationValue, ForceMode.Acceleration);
		}

		if (m_stateMachine.Rb.velocity.magnitude > m_stateMachine.MaxVelocity)
		{
            m_stateMachine.Rb.velocity = m_stateMachine.Rb.velocity.normalized;
            m_stateMachine.Rb.velocity *= m_stateMachine.MaxVelocity;
		}

		/*
		///GET DIRECTION ANGLE
		//Add all dir vector
		Vector3 dir = frontDir + rightDir;
		//Normalize it
		dir.Normalize();
		//Find the angle
		float angle = (Mathf.Asin(dir.y));
		Debug.Log("angle: " + angle * Mathf.Rad2Deg);
		//Vector3 result = Vector3.Add(testx, testy);

		//Debug.Log("result: " + result);
		*/

		// print(Rb.velocity.magnitude);


		/// SOLUTION !!!!!
		//(Composnate en X / Taille de votre vectuer) * vitesse de déplacement latéral + (composante en Y / taille de votre vectuer) * vitesse de déplacement (avant/arrière)

	}

	public Vector3 GetVectorOnFloor(Vector3 dir)
	{
		Vector3 vecOnFloor = Vector3.ProjectOnPlane(dir, Vector3.up);
		vectorOnFloor.Normalize();
		return vecOnFloor;
	}

	public override void OnExit()
	{
        Debug.Log("Exit state: FreeState\n");
    }

    public override bool CanEnter()
    {
		//Can enter only if touching ground

		return m_stateMachine.IsInContactWithFloor();	
    }
    public override bool CanExit()
    {
		return true;
    }
}
