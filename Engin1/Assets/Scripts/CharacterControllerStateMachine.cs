using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : MonoBehaviour
{
    public Camera Camera { get; private set; }
	public Rigidbody Rb { get; private set; }
	[field:SerializeField] public float AccelerationValue { get; private set; }
	[field: SerializeField] public float MaxVelocity { get; private set; }



	private CharacterState m_currentState;
	private List<CharacterState> m_possibleStates;


	private void Awake()
	{
		m_possibleStates = new List<CharacterState>();

		m_possibleStates.Add(new FreeState());
	}
	// Start is called before the first frame update
	void Start()
    {
        Camera = Camera.main;
        Rb = GetComponent<Rigidbody>();

        foreach(CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }

		m_currentState = m_possibleStates[0];
		m_currentState.OnEnter();
	}

 

    private void FixedUpdate()
    {
		m_currentState.OnFixedUpdate();
        
	}
}

//TODO
//Appliquer les déplacement relati a la cam dans les 3 autre direction
//Avec des vitesse de déplacement maximale différentes vers les cöté et vers l'arrière
//Lorsqu'aucun input est mis, décélérer le personnage rapidement