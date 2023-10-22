using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static VfxManager;

public class HitManager : MonoBehaviour
{
    [SerializeField] private CharacterControllerStateMachine m_controllerSM;
    [SerializeField] private bool m_canHit;
    [SerializeField] private bool m_canGetHit;
	[SerializeField] private GameObject m_prefabAudioSource;
	[SerializeField] protected EAgentType m_agentType = EAgentType.Count;

    [SerializeField] protected List<EAgentType> m_affectedAgentType = new List<EAgentType>();
    [SerializeField] private AudioClip m_punchAudioClip;



	public enum EAgentType
    {
        Ally,
        Enemy,
        Neutral,
        Count
    }
	

    
    public void GetHit(string hitter)
    {
        Debug.Log(this.transform.name + " just got hit by: " + hitter);
        if (m_controllerSM != null)
        {
			m_controllerSM.IsGettingHit = true;
		}
		
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherHitBox = other.GetComponent<HitManager>();
        if(otherHitBox == null) { return; }

        if (CanHitOther(otherHitBox))
        {     
            VfxManager._Instance.InstantiateVFX(EVFX_Type.Hit, other.ClosestPoint(transform.position));
            
            //Audio
            GameObject tempAudioSource = Instantiate(m_prefabAudioSource, other.ClosestPoint(transform.position), Quaternion.identity);
            tempAudioSource.GetComponent<AudioSource>().PlayOneShot(m_punchAudioClip);

			otherHitBox.GetHit(this.transform.name);
        }
    }



	private bool CanHitOther(HitManager other)
	{
		if (m_canHit && other.m_canGetHit)
		{
			return m_affectedAgentType.Contains(other.m_agentType);
		}
		return false;
	}
}
