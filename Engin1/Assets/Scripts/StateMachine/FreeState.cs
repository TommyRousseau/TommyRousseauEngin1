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
		Vector2 animationDir = Vector2.zero;
		Vector3 currentDir = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
		
            vectorOnFloor = GetVectorOnFloor(m_stateMachine.Camera.transform.forward);
			currentDir += vectorOnFloor;
			animationDir.y += 1;


			
			/*
			//Current velocity vector projected on the forward vector gives the value of the forward speed
			float forwardSpeed = Vector3.Dot(m_stateMachine.Camera.transform.forward, m_stateMachine.Rb.velocity);
			
			if (forwardSpeed < m_stateMachine.ForwardMaxVelocity)
			{
				m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
			}	
			*/
		}

		if (Input.GetKey(KeyCode.S))
		{
			vectorOnFloor = GetVectorOnFloor(-m_stateMachine.Camera.transform.forward);
			currentDir += vectorOnFloor;
			animationDir.y -= 1;
			/*
			float backSpeed = Vector3.Dot(m_stateMachine.Rb.velocity, -m_stateMachine.Camera.transform.forward);
			if (backSpeed < m_stateMachine.BackMaxVelocity)
			{
				m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
			}
			*/
		}

		if (Input.GetKey(KeyCode.D))
		{
			vectorOnFloor = GetVectorOnFloor(m_stateMachine.Camera.transform.right);
			currentDir += vectorOnFloor;
			animationDir.x += 1;
			/*
			float rightSpeed = Vector3.Dot(m_stateMachine.Camera.transform.right, m_stateMachine.Rb.velocity);
			if (rightSpeed < m_stateMachine.SideMaxVelocity)
			{
				m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
			}
			*/

		}

		if (Input.GetKey(KeyCode.A))
		{
			vectorOnFloor = GetVectorOnFloor(-m_stateMachine.Camera.transform.right);
			currentDir += vectorOnFloor;
			animationDir.x -= 1;
			/*
			float leftSpeed = Vector3.Dot(m_stateMachine.Rb.velocity, -m_stateMachine.Camera.transform.right);
			if (leftSpeed < m_stateMachine.SideMaxVelocity)
			{
				m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
			}
			*/
		}


		m_stateMachine.m_animator.SetFloat("MoveX", animationDir.x);
		m_stateMachine.m_animator.SetFloat("MoveY", animationDir.y);
		//float maxSpeed = (Mathf.Abs(currentDir.x) + Mathf.Abs(currentDir.z)) /2;
		currentDir.Normalize();
		//Vector3 fwdSpeed = Vector3.Dot(rigidbody.velocity, transform.forward);

		//Debug.Log("currentDir.Normaliz: " + currentDir);

		float maxSpeed = Mathf.Abs(currentDir.x) * m_stateMachine.SideMaxVelocity + Mathf.Abs(currentDir.z) * m_stateMachine.ForwardMaxVelocity;
		//float maxSpeed = currentDir.x * m_stateMachine.SideMaxVelocity + currentDir.z * m_stateMachine.ForwardMaxVelocity;
		
		
		//Debug.Log("Max Speed: " +maxSpeed);
		if(m_stateMachine.Rb.velocity.magnitude < maxSpeed)
		{
			//Debug.Log("TEST");
			m_stateMachine.Rb.AddForce(currentDir * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
		}
		
		//Debug.Log("Max Speed: " + maxSpeed);
		//Debug.Log("Current Speed: " + m_stateMachine.Rb.velocity.magnitude);

		/// SOLUTION !!!!!
		//(Composnate en X / Taille de votre vecteur) * vitesse de déplacement latéral + (composante en Y / taille de votre vectuer) * vitesse de déplacement (avant/arrière)



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



	}

	public Vector3 GetVectorOnFloor(Vector3 dir)
	{
		Vector3 vecOnFloor = Vector3.ProjectOnPlane(dir, Vector3.up);
		//vectorOnFloor.Normalize();
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
