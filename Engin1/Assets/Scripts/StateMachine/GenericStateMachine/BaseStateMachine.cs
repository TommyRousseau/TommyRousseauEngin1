using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine<T> : MonoBehaviour where T : IState
{
    protected T m_currentState;
    protected List<T> m_possibleStates;

    protected virtual void Awake()
    {
        CreatePossibleStates();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        foreach (IState state in m_possibleStates)
        {
            state.OnStart();
        }

        m_currentState = m_possibleStates[m_possibleStates.Count - 1];
        m_currentState.OnEnter();
    }

    protected virtual void Update()
    {
        m_currentState.OnUpdate();
        TryStateTransition();
    }
    protected virtual void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }


    protected virtual void CreatePossibleStates()
    {

    }

    private void TryStateTransition()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }
       
        //If I can leave current state
        foreach (var state in m_possibleStates)
        {
            if (m_currentState.Equals(state))
            {
                continue;
            }

            if (state.CanEnter(m_currentState))
            {
                m_currentState.OnExit();
                m_currentState = state;
                m_currentState.OnEnter();
                return;
            }
        }
    }
}
