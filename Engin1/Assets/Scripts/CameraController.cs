using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float m_mousePosX;
    //Vector2 currentPosArroundObject = Vector2.zero;
    private float m_currentAngleX = 0;
    private float m_currentAngleY = 0;
    [SerializeField] private Transform m_lookAt;
    [SerializeField] private float m_rotationSpeedX;
    [SerializeField] private float m_rotationSpeedY;
    [SerializeField] private float m_minDistanceFromPlayer;
    [SerializeField] private float m_maxDistanceFromPlayer;
    [SerializeField] private float m_scrollSpeed;
    [SerializeField] private Vector2 m_YRotationLimits;
    

    private void Awake()
    {
        //currentPosArroundObject = new Vector2(0, -1);
    }

    void Update()
    {
        //TODO SÉPARÉ LES MOUVEMENT EN FONCTION


        m_currentAngleX = Input.GetAxis("Mouse X");
        transform.RotateAround(m_lookAt.position, m_lookAt.transform.up, m_currentAngleX * m_rotationSpeedX * Time.deltaTime);

        m_currentAngleY = Input.GetAxis("Mouse Y");

        //Adjust rotation if angle is under 0
        float xAngle = transform.eulerAngles.x;
        xAngle = ClampAngle(xAngle);


        //Rotate only if rotation is between limits
        if (m_currentAngleY > 0 && xAngle < m_YRotationLimits.y
            || (m_currentAngleY < 0 && xAngle > m_YRotationLimits.x))
        {
            transform.RotateAround(m_lookAt.position, transform.right, m_currentAngleY * m_rotationSpeedY * Time.deltaTime);
        }
       

        //MOUSE SCROLL
        float mouseScroll = Input.mouseScrollDelta.y;
        float camDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);

        if ((mouseScroll > 0 && camDistance > m_minDistanceFromPlayer)
            || (mouseScroll < 0 && camDistance < m_maxDistanceFromPlayer))
        {
            transform.Translate(Vector3.forward * (mouseScroll * m_scrollSpeed));
        }      
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }
}


/*
      m_currentAngleX = Input.GetAxis("Mouse X");
        transform.RotateAround(m_lookAt.position, m_lookAt.transform.up, m_currentAngleX * m_rotationSpeedX * Time.deltaTime);

        m_currentAngleY = Input.GetAxis("Mouse Y");
       
       
        transform.RotateAround(m_lookAt.position, transform.right, m_currentAngleY * m_rotationSpeedY * Time.deltaTime);

        float xRotationValue = transform.eulerAngles.x;
        print(xRotationValue);

        
        xRotationValue = Mathf.Clamp(xRotationValue, m_clampingXRotationValue.x, m_clampingXRotationValue.y);

        Vector3 clampedRotationEuler = new Vector3(xRotationValue, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles = clampedRotationEuler;
*/