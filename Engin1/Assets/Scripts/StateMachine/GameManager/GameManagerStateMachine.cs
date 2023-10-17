using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.N))
        {
            //Cheat pour changer de scene
            SceneManager.LoadScene("Test", LoadSceneMode.Additive);
        }
    }
}
