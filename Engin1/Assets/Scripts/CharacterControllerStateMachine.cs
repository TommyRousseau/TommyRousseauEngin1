using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : MonoBehaviour
{
    public float speed;
	[SerializeField] public Animator m_animator;
	public Camera Camera { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    
	[field:SerializeField] public float AccelerationValue { get; private set; }
	[field: SerializeField] public float ForwardMaxVelocity { get; private set; }
	[field: SerializeField] public float SideMaxVelocity { get; private set; }
	[field: SerializeField] public float BackMaxVelocity { get; private set; }
    [field: SerializeField] public float JumpIntensity { get; private set; }
	[field: SerializeField] public float TimeOnGround { get; set; }
	[field: SerializeField] public float TimeGettingUp { get; set; }



	private CharacterState m_currentState;
	private List<CharacterState> m_possibleStates;
    [SerializeField] private CharacterFloorTrigger m_floorTrigger;

    private void Awake()
	{
		m_possibleStates = new List<CharacterState>();

		m_possibleStates.Add(new JumpState());
		m_possibleStates.Add(new HitState());
		m_possibleStates.Add(new OnGroundState());
		m_possibleStates.Add(new GettingUpState());
		m_possibleStates.Add(new AttackingState());
		m_possibleStates.Add(new FreeState());

		foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }

    }
	// Start is called before the first frame update
	void Start()
    {
        Camera = Camera.main;
       

       

		m_currentState = m_possibleStates[m_possibleStates.Count-1];
		m_currentState.OnEnter();
	}

    private void UpdateAnimatorValues()
    {

        m_animator.SetBool("TouchGround", IsInContactWithFloor());

	}
 

    private void FixedUpdate()
    {
		m_currentState.OnFixedUpdate();
        
	}

    private void Update()
    {
        speed = Rb.velocity.magnitude;
		//
		UpdateAnimatorValues();
        //

        m_currentState.OnUpdate();
        TryStateTransition();
    }

	private void TryStateTransition()
	{
        if (!m_currentState.CanExit())
        {
            return;
        }

        //Je PEUX quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState == state)
            {
                continue;
            }

            if (state.CanEnter())
            {
                //Quitter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer das le state state
                m_currentState.OnEnter();
                return;
            }
        }
    }

    public bool IsInContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    }
}

//TODO
//Appliquer les déplacement relatif a la cam dans les 3 autre direction
//Avec des vitesse de déplacement maximale différentes vers les cöté et vers l'arrière
//Lorsqu'aucun input est mis, décélérer le personnage rapidement