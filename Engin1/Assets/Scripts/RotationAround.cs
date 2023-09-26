using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAround : MonoBehaviour
{
	[SerializeField] float m_rotationSpeed;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.RotateAround(transform.position, Vector3.up, m_rotationSpeed * Time.deltaTime);
	}
}
