using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    [SerializeField] private GameObject m_hitbox;
    [SerializeField] private CharacterControllerStateMachine m_characterController;

    public void ActivateHitbox()
    {
        m_characterController.ToggleAttackHitbox(true);
       
    }
    public void DeactivateHitbox()
    {
        m_characterController.ToggleAttackHitbox(false);
       
    }

}
