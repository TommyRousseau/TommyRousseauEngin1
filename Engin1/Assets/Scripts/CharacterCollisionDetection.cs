using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisionDetection : MonoBehaviour
{
	[field: SerializeField] public bool CollisionOccurred { get; private set; }

	public virtual void FixedUpdate()
	{
		CollisionOccurred = false;
	}

	public virtual void OnCollisionStay(Collision c)
	{
		if(c.gameObject.layer == 9)
		{
			CollisionOccurred = true;
		}
		
	}

	

}
