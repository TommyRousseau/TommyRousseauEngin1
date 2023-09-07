using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float m_mousePosX;
    //Vector2 currentPosArroundObject = Vector2.zero;
    private float m_currentAngleX = 0;
    private float m_currentAngleY = 0;
    private float m_mouseScroll = 0;
    private float m_xCamAngle = 0;
    private float m_playerRegistredDistance = 0;
    [SerializeField] private Transform m_lookAt;
    [SerializeField] private float m_rotationSpeedX;
    [SerializeField] private float m_rotationSpeedY;
    [SerializeField] private float m_minDistanceFromPlayer;
    [SerializeField] private float m_maxDistanceFromPlayer;
    [SerializeField] private float m_scrollSpeed;
    [SerializeField] private Vector2 m_YRotationLimits;


    //GDD
    //Hauter
    //préparation
    //Déplacemnt en air
    //Conserver momentum




    private void Awake()
    {
        //Get the default distance of the camera
        m_playerRegistredDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
    }

	private void FixedUpdate()
	{
		ReplaceCamBeforeObstructionFUpdate();
	}

	void Update()
    {

        //Horizontal camera rotation
		m_currentAngleX = Input.GetAxis("Mouse X");
        if(m_currentAngleX !=0)
        {
            CamHorizontalAroundTarget();
		}
       

        //Vertical camera rotataion
        m_currentAngleY = - Input.GetAxis("Mouse Y");

        //Adjust rotation if camera vertical angle is under 0
        m_xCamAngle = ClampAngle(transform.eulerAngles.x);
		CorrectIfOverpassLimits();

		if (m_currentAngleX != 0)
		{
			CamVerticalAroundTarget();
		}

		//MOUSE SCROLL
		m_mouseScroll = Input.mouseScrollDelta.y;

		if (m_mouseScroll != 0)
		{
			AdjustDistance();
		}
		
    }

    private void CorrectIfOverpassLimits()
    {
		if (m_xCamAngle < m_YRotationLimits.x - 5)
		{
			transform.RotateAround(m_lookAt.position, transform.right, 0.1f * m_rotationSpeedY * Time.deltaTime);
		}
		if (m_xCamAngle > m_YRotationLimits.y + 5)
		{
			transform.RotateAround(m_lookAt.position, transform.right, -0.1f * m_rotationSpeedY * Time.deltaTime);
		}
	}
    private void CamHorizontalAroundTarget()
    {
		transform.RotateAround(m_lookAt.position, m_lookAt.transform.up, m_currentAngleX * m_rotationSpeedX * Time.deltaTime);
	}

    private void CamVerticalAroundTarget()
    {
		//Adjust rotation if camera vertical angle is under 0
		float xCamAngle = transform.eulerAngles.x;
		xCamAngle = ClampAngle(xCamAngle);


		//Rotate only if rotation is between limits
		if (m_currentAngleY > 0 && xCamAngle < m_YRotationLimits.y
			|| (m_currentAngleY < 0 && xCamAngle > m_YRotationLimits.x))
		{
			transform.RotateAround(m_lookAt.position, transform.right, m_currentAngleY * m_rotationSpeedY * Time.deltaTime);
		}
	}

    private void AdjustDistance()
    {


        /* // For world z movement
            var vectorOnFloor = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            vectorOnFloor.Normalize();	
        */

       // float camDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
       
   

		if ((m_mouseScroll > 0 && m_playerRegistredDistance > m_minDistanceFromPlayer)
			|| (m_mouseScroll < 0 && m_playerRegistredDistance < m_maxDistanceFromPlayer))
		{
            //transform.Translate(vectorOnFloor * (m_mouseScroll * m_scrollSpeed), Space.World); //for world z movement
            transform.Translate(Vector3.forward * (m_mouseScroll * m_scrollSpeed));
            m_playerRegistredDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
            // m_playerRegistredDistance = camDistance;
        }
	}

	private void ReplaceCamBeforeObstructionFUpdate()
    {
       
        int layerMask = 1 << 8;

		RaycastHit hit;
        var vecteurDiff = transform.position - m_lookAt.position;
        var distance = vecteurDiff.magnitude;
        bool needToReposition = false;
        Vector3 newPos;

        if (Physics.Raycast(m_lookAt.position, vecteurDiff.normalized, out hit, m_playerRegistredDistance, layerMask))
        {
            
            //There's an object between target and camera
            Debug.DrawRay(m_lookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            //Replace the camera at the cast collision point
            transform.SetPositionAndRotation(hit.point, transform.rotation);

            //The camera will need to be repositioned when no more obtruction
            needToReposition = true;
 
        }
        else
        {
            
            if(m_playerRegistredDistance != 0 && needToReposition)
            {
                newPos = transform.position - m_lookAt.position;
                newPos.Normalize();
                newPos *= m_playerRegistredDistance;

                transform.SetPositionAndRotation(newPos, transform.rotation);
                print("needToReposition FALSE");
                needToReposition = false;
            }

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
