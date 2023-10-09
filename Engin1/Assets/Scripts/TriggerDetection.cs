using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
	[field: SerializeField] public bool CollisionOccurred { get; private set; }

	public virtual void FixedUpdate()
	{
		CollisionOccurred = false;
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer != 0)
		{
			CollisionOccurred = true;
		}
	}
}
