
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;    //Time to get off the ground
    private float m_currentStateTimer = 0.0f;

	Vector3 vectorOnFloor = new Vector3();

	public override void OnEnter()
    {

		m_stateMachine.Animator.SetTrigger("Jump");
		m_currentStateTimer = STATE_EXIT_TIMER;
		//Jump
		m_stateMachine.Rb.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
       
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override void OnFixedUpdate()
    {
		

	}
	
	public override void OnExit()
    {
      
    }

    public override bool CanEnter(IState currentState)
    {

        if( Input.GetKey(KeyCode.Space))
		{
			if (currentState is FreeState)
			{
				return m_stateMachine.IsInContactWithFloor();
			}
		}

		return false;
			
	}

    public override bool CanExit()
    {
        if(m_currentStateTimer <=0.0f)
        {
            return true;
        }
        return false;
    }
}
