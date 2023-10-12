using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HitManager : MonoBehaviour
{
    [SerializeField] private bool m_canHit;
    [SerializeField] private bool m_canGetHit;
    [SerializeField] protected EAgentType m_agentType = EAgentType.Count;

    [SerializeField] protected List<EAgentType> m_affectedAgentType = new List<EAgentType>();
    public enum EAgentType
    {
        Ally,
        Enemy,
        Neutral,
        Count
    }

    public bool IsGettingHit
    {
        get {bool temp = m_isGettingHit; m_isGettingHit = false; return m_isGettingHit;  }
        set { m_isGettingHit = value; }
    }
    private bool m_isGettingHit;

    
    public void GetHit(string hitter)
    {
        Debug.Log(this.transform.name + " just got hit by: " + hitter);
        IsGettingHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherHitBox = other.GetComponent<HitManager>();
        if(otherHitBox == null) { return; }

        if (CanHitOther(otherHitBox))
        {
            otherHitBox.GetHit(this.transform.name);
        }
    }

    private bool CanHitOther(HitManager other)
    {
        print("11");
        if(m_canHit && other.m_canGetHit)
        {
            print("22:" + m_affectedAgentType.Contains(other.m_agentType));
            return m_affectedAgentType.Contains(other.m_agentType);
        }
        return false;
    }
}
