using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
	[SerializeField] private AudioClip m_jumpAudioClip;
    [SerializeField] private AudioClip m_landingAudioClip;
    [SerializeField] private AudioClip m_punchAudioClip;
    [SerializeField] private List<AudioClip> m_walkingClip;

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

			case EActionType.Punch:
				m_audioSource.PlayOneShot(m_punchAudioClip);
				break;

			case EActionType.Walk:
				m_audioSource.PlayOneShot(m_walkingClip[Random.Range(0,2)]);
				break;

			case EActionType.Count:
                Debug.LogWarning("Character Audio Controller ERROR");
                break;

        }

    }




	/*
	public void PlaySFX(bool shoulMoveToLocation, Vector3 location)
	{

	}
	*/

}



public enum EActionType
{
	Jump,
	Landing,
	Punch,
	Walk,
	Count
}

