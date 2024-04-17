using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : InteractableObjectBase
{
    public override void Interact()
    {
        Debug.Log("yo it's work " + this.gameObject.name);

    }
}
