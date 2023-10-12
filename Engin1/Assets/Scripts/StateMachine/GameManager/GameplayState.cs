using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IState
{
    protected Camera m_camera;

    public GameplayState (Camera camera)
    {
        m_camera = camera;
    }
    bool IState.CanEnter(IState currentState)
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    bool IState.CanExit()
    {
       
        return Input.GetKeyDown(KeyCode.G);
    }

    void IState.OnEnter()
    {
        m_camera.enabled = true;
    }

    void IState.OnExit()
    {
        m_camera.enabled = false;
    }

    void IState.OnFixedUpdate()
    {

    }

    void IState.OnStart()
    {

    }

    void IState.OnUpdate()
    {

    }

}
