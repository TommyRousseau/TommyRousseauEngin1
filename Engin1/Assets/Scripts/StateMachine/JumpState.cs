
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;

	Vector3 vectorOnFloor = new Vector3();

	public override void OnEnter()
    {
        Debug.Log("Enter state: JumpState\n");
        m_stateMachine.Animator.SetTrigger("Jump");
        //Effectuer le saut
        m_stateMachine.Rb.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {
		Vector3 currentDir = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
		{

			vectorOnFloor = GetVectorOnFloor(m_stateMachine.Camera.transform.forward);
			currentDir += vectorOnFloor;

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

			/*
			float leftSpeed = Vector3.Dot(m_stateMachine.Rb.velocity, -m_stateMachine.Camera.transform.right);
			if (leftSpeed < m_stateMachine.SideMaxVelocity)
			{
				m_stateMachine.Rb.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
			}
			*/
		}

		currentDir.Normalize();


		float maxSpeed = Mathf.Abs(currentDir.x) * m_stateMachine.SideMaxVelocity + Mathf.Abs(currentDir.z) * m_stateMachine.ForwardMaxVelocity;
		//Debug.Log(maxSpeed);
		if (m_stateMachine.Rb.velocity.magnitude < maxSpeed)
		{
			m_stateMachine.Rb.AddForce(currentDir * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
		}

		//Debug.Log("Max Speed: " + maxSpeed);
		//Debug.Log("Current Speed: " + m_stateMachine.Rb.velocity.magnitude);

		/// SOLUTION !!!!!
		//(Composnate en X / Taille de votre vectuer) * vitesse de déplacement latéral + (composante en Y / taille de votre vectuer) * vitesse de déplacement (avant/arrière)


	}

	public Vector3 GetVectorOnFloor(Vector3 dir)
	{
		Vector3 vecOnFloor = Vector3.ProjectOnPlane(dir, Vector3.up);
		//vectorOnFloor.Normalize();
		return vecOnFloor;
	}
	
	public override void OnExit()
    {
        Debug.Log("Exit state: JumpState\n");
    }

    public override bool CanEnter(CharacterState currentState)
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override bool CanExit()
    {
        if(m_currentStateTimer <=0.0f)
        {
			Debug.Log("TESTING JUMP");
            return true;
        }
        return false;
    }
}
