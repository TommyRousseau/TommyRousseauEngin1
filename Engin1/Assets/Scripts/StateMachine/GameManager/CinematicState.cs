using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : IState
{
    protected Camera m_camera;

    public CinematicState (Camera camera)
    {
        m_camera = camera;
    }

    bool IState.CanEnter(IState currentState)
    {
        return false;
    }

    bool IState.CanExit()
    {
        return false;
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
