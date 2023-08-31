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

	private void FixedUpdate()
	{
		ReplaceCamBeforeObstructionFUpdate();
	}

	void Update()
    {
		//TODO - SÉPARÉ LES MOUVEMENT EN FONCTION
        //Faire que la cam garde ça distance avant de se coler au mur et la remttre en placer lorsqu'il n'y a plus de mur.
		

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

    private void ReplaceCamBeforeObstructionFUpdate()
    {
        int layerMask = 1 << 8;

		RaycastHit hit;
        var vecteurDiff = transform.position - m_lookAt.position;
        var distance = vecteurDiff.magnitude;

		if (Physics.Raycast(m_lookAt.position, vecteurDiff.normalized, out hit, distance, layerMask))
        {
            //There's an object between target and camera
            Debug.DrawRay(m_lookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
   
            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }
        else
        {
			Debug.DrawRay(m_lookAt.position, vecteurDiff.normalized * hit.distance, Color.white);
			
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
