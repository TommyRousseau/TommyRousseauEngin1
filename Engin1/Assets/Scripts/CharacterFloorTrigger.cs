using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public bool IsOnFloor { get; private set;}

	public virtual void FixedUpdate()
	{
		IsOnFloor = false;
	}

	private void OnTriggerStay(Collider other)
    {
		if (other.gameObject.layer != 9)
        {
			IsOnFloor = true;
		}			
    }
}
