using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : BaseStateMachine<CharacterState>
{
	[SerializeField] public Animator m_animator;
	public Camera Camera { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public TriggerDetection HitDetector { get; private set; }
	[field:SerializeField] public float AccelerationValue { get; private set; }
	[field:SerializeField] public float AirAccelerationValue { get; private set; }
	[field:SerializeField] public float StopSpeed { get; private set; }
	[field: SerializeField] public float ForwardMaxVelocity { get; private set; }
	[field: SerializeField] public float SideMaxVelocity { get; private set; }
	[field: SerializeField] public float BackMaxVelocity { get; private set; }
    [field: SerializeField] public float JumpIntensity { get; private set; }
	[field: SerializeField] public float TimeOnGround { get; private set; }
	[field: SerializeField] public float TimeGettingUp { get; private set; }
	[field: SerializeField] public float HitTime { get; private set; }
	[field: SerializeField] public float AttackTime { get; private set; }
	[field: SerializeField] public float MaxJumpFall { get; private set; }
	public float JumpHeight { get; set; }
	public bool IsFallStuned { get; set; }

	[SerializeField] private CharacterFloorTrigger m_floorTrigger;
	

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<CharacterState>();

        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new HitState());
        m_possibleStates.Add(new OnGroundState());
        m_possibleStates.Add(new GettingUpState());
        m_possibleStates.Add(new AttackingState());
        m_possibleStates.Add(new FreeState());

       
    }

    protected override void Start()
    {
        //base.Start();
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart();
        }
        Camera = Camera.main;
        
	}

    private void UpdateAnimatorValues()
    {
        m_animator.SetBool("TouchGround", IsInContactWithFloor());
	}
 

    protected override void FixedUpdate()
    {
		base.FixedUpdate();
        
	}

    protected override void Update()
    {	
        UpdateAnimatorValues();
    }

    public bool IsInContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    }

	public bool IsGettingHit()
	{
        return HitDetector.CollisionOccurred;
	}
}
