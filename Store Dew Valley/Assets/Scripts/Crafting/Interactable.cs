using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{


    public virtual void Interac(int value)
    {

        Debug.Log("Interact " + transform.name);
    }
}
