using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource m_tempAudioSource;
    private bool m_hasStartedPlaying;
 
    void Update()
    {
        if (m_tempAudioSource.isPlaying)
        {
            m_hasStartedPlaying = true;
		}
        else
        {
            if (m_hasStartedPlaying)
            {
                Destroy(this.gameObject);
            }
        }

	}
}
