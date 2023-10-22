using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
	[SerializeField] private AudioClip m_jumpAudioClip;
    [SerializeField] private AudioClip m_landingAudioClip;

    public void PlaySound(EActionType actionType)
    {

        switch(actionType)
        {
            case EActionType.Jump:
                m_audioSource.PlayOneShot(m_jumpAudioClip);
				break;

            case EActionType.Landing:
				m_audioSource.PlayOneShot(m_landingAudioClip);
				break;

            case EActionType.Count:
                Debug.LogWarning("Character Audio Controller ERROR");
                break;

        }

    }


	public void PlaySFX(bool shoulMoveToLocation, Vector3 location)
	{

	}

}



public enum EActionType
{
	Jump,
	Landing,
	Count
}

