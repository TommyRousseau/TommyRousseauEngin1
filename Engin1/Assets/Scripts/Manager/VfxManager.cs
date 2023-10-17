using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager _Instance
    {
        get;
        protected set;
    }

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(_Instance != this)
        {
            Destroy(gameObject);
        }
     
    }

    [SerializeField] private GameObject m_hitPS;

    public void InstantiateVFX(EVFX_Type vfxType, Vector3 pos)
    {
        switch (vfxType)
        {
            case EVFX_Type.Hit:
                Instantiate(m_hitPS, pos,Quaternion.identity, transform);
                    break;
            default:
                break;
        }
    }

   
    public enum EVFX_Type
    {
        Hit,
        Count
    }
}
