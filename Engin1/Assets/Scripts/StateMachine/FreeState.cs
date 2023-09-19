using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeState : CharacterState
{
	Vector3 vectorOnFloor = new Vector3();

	float m_lerpBlendXTimer = 0;
    float m_animXBlend = 0;
    float m_animXStart = 0;
    float m_animXGoal;

    float m_lerpBlendYTimer = 0;	
	float m_animYBlend;
    float m_animYStart = 0;
    float m_animYGoal;


    public override void OnEnter()
	{
		Debug.Log("Enter state: FreeState\n");
	}


    public override void OnUpdate()
	{
		
		if(m_animXBlend != m_animXGoal)
		{
            m_animXBlend = Mathf.Lerp(m_animXStart, m_animXGoal, m_lerpBlendXTimer);
            m_lerpBlendXTimer += Time.deltaTime;
            m_stateMachine.m_animator.SetFloat("MoveX", m_animXBlend);
        }

        if (m_animYBlend != m_animYGoal)
        {
            m_animYBlend = Mathf.Lerp(m_animYStart, m_animYGoal, m_lerpBlendYTimer);
            m_lerpBlendYTimer += Time.deltaTime;
            m_stateMachine.m_animator.SetFloat("MoveY", m_animYBlend);
        }


        //m_stateMachine.m_animator.SetFloat("MoveY", m_animYBlend);

    }

	public override void OnFixedUpdate()
	{
		Vector3 currentDir = Vector3.zero;
		Vector3 speedVector =Vector3.zero;
		
		if (Input.GetKey(KeyCode.W))
		{
		
			currentDir += m_stateMachine.Camera.transform.forward;

            speedVector.z += 1;

        }

		if (Input.GetKey(KeyCode.S))
		{
			currentDir += -m_stateMachine.Camera.transform.forward;
            speedVector.z -= 1;

        }

		if (Input.GetKey(KeyCode.D))
		{
			currentDir += m_stateMachine.Camera.transform.right;
            speedVector.x += 1;


        }

		if (Input.GetKey(KeyCode.A))
		{
			currentDir += -m_stateMachine.Camera.transform.right;
            speedVector.x -= 1;
   
        }

        //For controllers
        //speedVector.z = (Input.GetAxis("Horizontal"));
        //speedVector.x = (Input.GetAxis("Vertical"));


        //Animator
        if (speedVector.x != m_animXGoal)
        {
            m_animXStart = m_stateMachine.m_animator.GetFloat("MoveX");
            m_animXGoal = speedVector.x;
            m_lerpBlendXTimer = 0;
        }

        if (speedVector.z != m_animYGoal)
        {
            m_animYStart = m_stateMachine.m_animator.GetFloat("MoveY");
            m_animYGoal = speedVector.z;
            m_lerpBlendYTimer = 0;
        }



        speedVector.Normalize();

		//Set max speed for each direction
		speedVector.x = speedVector.x * m_stateMachine.SideMaxVelocity;
		if (speedVector.z>0)
		{
            speedVector.z = speedVector.z * m_stateMachine.ForwardMaxVelocity;
        }
		else if(speedVector.z<0)
		{
            speedVector.z = speedVector.z * m_stateMachine.BackMaxVelocity;
        }


		




        currentDir = Vector3.ProjectOnPlane(currentDir, Vector3.up);
        currentDir.Normalize();

        float maxSpeed = Mathf.Sqrt((speedVector.x * speedVector.x) + (speedVector.z * speedVector.z));


		if (m_stateMachine.Rb.velocity.magnitude < maxSpeed)
		{
            m_stateMachine.Rb.AddForce(currentDir * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
        }

		if(maxSpeed == 0 && m_stateMachine.Rb.velocity.magnitude > 1)
		{
			m_stateMachine.Rb.AddForce(-m_stateMachine.Rb.velocity * m_stateMachine.StopSpeed);

        }

        Debug.Log("CurrentSpeed: " + m_stateMachine.Rb.velocity.magnitude);
    }


	public override void OnExit()
	{
        Debug.Log("Exit state: FreeState\n");
    }

    public override bool CanEnter(CharacterState currentState)
    {
        
		if(currentState is JumpState)
		{
            return m_stateMachine.IsInContactWithFloor();
        }

        if (currentState is AttackingState)
        {
            return m_stateMachine.IsInContactWithFloor();
        }

        return false;
		
		


    }
    public override bool CanExit()
    {
		return true;
    }
}
