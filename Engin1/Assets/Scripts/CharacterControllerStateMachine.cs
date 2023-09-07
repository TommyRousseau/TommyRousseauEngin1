using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : MonoBehaviour
{
    public Camera Camera { get; private set; }
	public Rigidbody Rb { get; private set; }
    
	[field:SerializeField] public float AccelerationValue { get; private set; }
	[field: SerializeField] public float MaxVelocity { get; private set; }
    [field: SerializeField] public float JumpIntensity { get; private set; }



    private CharacterState m_currentState;
	private List<CharacterState> m_possibleStates;
    [SerializeField] private CharacterFloorTrigger m_floorTrigger;

    private void Awake()
	{
		m_possibleStates = new List<CharacterState>();

		m_possibleStates.Add(new FreeState());
		m_possibleStates.Add(new JumpState());

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }

    }
	// Start is called before the first frame update
	void Start()
    {
        Camera = Camera.main;
        Rb = GetComponent<Rigidbody>();

       

		m_currentState = m_possibleStates[0];
		m_currentState.OnEnter();
	}

 

    private void FixedUpdate()
    {
		m_currentState.OnFixedUpdate();
        
	}

    private void Update()
    {
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
//Appliquer les déplacement relati a la cam dans les 3 autre direction
//Avec des vitesse de déplacement maximale différentes vers les cöté et vers l'arrière
//Lorsqu'aucun input est mis, décélérer le personnage rapidement