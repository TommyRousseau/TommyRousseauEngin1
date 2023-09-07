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
    private float xCamAngle = 0;
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
        //currentPosArroundObject = new Vector2(0, -1);
    }

	private void FixedUpdate()
	{
		ReplaceCamBeforeObstructionFUpdate();
	}

	void Update()
    {
        //TODO
        //Faire que la cam garde ça distance avant de se coler au mur et la remttre en placer lorsqu'il n'y a plus de mur.


        //Horizontal camera rotation
		m_currentAngleX = Input.GetAxis("Mouse X");
        if(m_currentAngleX !=0)
        {
            CamHorizontalAroundTarget();
		}
       

        //Vertical camera rotataion
        m_currentAngleY = - Input.GetAxis("Mouse Y");

		//Adjust rotation if camera vertical angle is under 0
		xCamAngle = ClampAngle(transform.eulerAngles.x);
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
		if (xCamAngle < m_YRotationLimits.x - 5)
		{
			transform.RotateAround(m_lookAt.position, transform.right, 0.1f * m_rotationSpeedY * Time.deltaTime);
		}
		if (xCamAngle > m_YRotationLimits.y + 5)
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

	
		var vectorOnFloor = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
		vectorOnFloor.Normalize();	
		

		float camDistance = Vector3.Distance(transform.position, m_lookAt.transform.position);


		if ((m_mouseScroll > 0 && camDistance > m_minDistanceFromPlayer)
			|| (m_mouseScroll < 0 && camDistance < m_maxDistanceFromPlayer))
		{
			transform.Translate(vectorOnFloor * (m_mouseScroll * m_scrollSpeed), Space.World);
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
