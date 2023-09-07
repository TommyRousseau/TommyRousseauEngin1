using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloorTrigger : MonoBehaviour
{
    public bool IsOnFloor { get; private set;}

    private void OnTriggerStay(Collider other)
    {
        if(!IsOnFloor)
        {
            Debug.Log("TOUCH GROUND");
        }
   
        IsOnFloor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("LEAVE GROUND");
        IsOnFloor = false;
    }

}
