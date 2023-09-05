using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private IState m_currentState;
    private List<IState> m_possibleStates;


    // Start is called before the first frame update
    void Start()
    {
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

	}

    // Update is called once per frame
    void Update()
    {
        m_currentState.OnUpdate();

	}
}
