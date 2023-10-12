using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerStateMachine : BaseStateMachine<IState>
{
    [SerializeField] private Camera m_gameplayCamera;
    [SerializeField] private Camera m_cinematicCamera;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        
    }
}
