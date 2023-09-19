using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float m_targetedDistance = 0;

    private float m_scrollLerpTimer;


    private float m_scrollLerpStartTime;
    private Vector3 m_lerpStartPosition = Vector3.zero;
    private Vector3 m_targetedPosition = Vector3.zero;


    [SerializeField] private Transform m_lookAt;
    [SerializeField] private float m_rotationSpeedX;
    [SerializeField] private float m_rotationSpeedY;
    [SerializeField] private float m_minDistanceFromPlayer;
    [SerializeField] private float m_maxDistanceFromPlayer;
    [SerializeField] private float m_scrollSpeed;
    [SerializeField] private float m_scrollDistance;
    [SerializeField] private Vector2 m_YRotationLimits;


    //GDD
    //Hauteur
    //préparation
    //Déplacemnt en air
    //Conserver momentum




    private void Awake()
    {
        //Get the default distance of the camera
        m_playerRegistredDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
        m_targetedDistance = m_playerRegistredDistance;

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


		if (m_currentAngleY != 0)
		{
			CamVerticalAroundTarget();
		}


		//MOUSE SCROLL
		m_mouseScroll = Input.mouseScrollDelta.y;

		if (m_mouseScroll != 0)
		{
			AdjustDistance();
		}
		

		if (m_playerRegistredDistance != m_targetedDistance)
        {
			m_playerRegistredDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
			LerpScroll();
			//m_playerRegistredDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);
			
			
		}

	}

	private void LerpScroll()
	{
        if(m_targetedDistance > m_playerRegistredDistance && m_playerRegistredDistance < m_maxDistanceFromPlayer)
        {
            DoTheLerp();
        }

		if (m_targetedDistance < m_playerRegistredDistance && m_playerRegistredDistance > m_minDistanceFromPlayer)
		{
			DoTheLerp();
		}
	}

    private void DoTheLerp()
    {
		if (m_currentAngleX != 0 || m_currentAngleY != 0)
		{
			UpdateScroll();
		}

		// Distance moved equals elapsed time times speed..
		float timeLerping = (Time.time - m_scrollLerpStartTime) * m_scrollSpeed;

		// Percetage completed of the lerp
		float percentDone = timeLerping / 1;

		transform.position = Vector3.Lerp(m_lerpStartPosition, m_targetedPosition, percentDone);

		if (percentDone >= 1)
		{
			transform.position = m_targetedPosition;
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
        //TODO
        //Lerp quand scroll
        m_scrollLerpStartTime = Time.time;

        m_lerpStartPosition = transform.position;

        GameObject temp = new GameObject();
        temp.transform.position = transform.position;
		temp.transform.rotation = transform.rotation;

		temp.transform.Translate(Vector3.forward * (m_mouseScroll * m_scrollDistance));
           m_targetedPosition = temp.transform.position;

			m_targetedDistance = Vector3.Distance(m_targetedPosition, m_lookAt.transform.position);
        Destroy(temp);
		
	}

    private void UpdateScroll()
    {
		//m_scrollLerpStartTime = Time.time;

		m_lerpStartPosition = transform.position;

		GameObject temp = new GameObject();
		temp.transform.position = transform.position;
		temp.transform.rotation = transform.rotation;

		temp.transform.Translate(Vector3.forward * (m_playerRegistredDistance - m_targetedDistance));
		m_targetedPosition = temp.transform.position;

		Destroy(temp);
		m_targetedDistance = Vector3.Distance(m_targetedPosition, m_lookAt.transform.position);
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
