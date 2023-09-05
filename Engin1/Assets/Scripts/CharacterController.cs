using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Camera m_camera;
    [SerializeField] private float m_accelerationValue;
    [SerializeField] private float m_maxVelocity;
    private Rigidbody m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_rb = GetComponent<Rigidbody>();
    }

 

    private void FixedUpdate()
    {
        /*
        var vectorOnFloor = Vector3.ProjectOnPlane(m_camera.transform.forward, Vector3.up);
        vectorOnFloor.Normalize();


        if (Input.GetKey(KeyCode.W))
        {
                m_rb.AddForce(vectorOnFloor * m_accelerationValue, ForceMode.Acceleration);  
        }
        
        if (m_rb.velocity.magnitude < m_maxVelocity)
        {
            m_rb.velocity = m_rb.velocity.normalized;
            m_rb.velocity *= m_maxVelocity;
        }
    
        print(m_rb.velocity.magnitude);
        */
    }
}

//TODO
//Appliquer les déplacement relati a la cam dans les 3 autre direction
//Avec des vitesse de déplacement maximale différentes vers les cöté et vers l'arrière
//Lorsqu'aucun input est mis, décélérer le personnage rapidement