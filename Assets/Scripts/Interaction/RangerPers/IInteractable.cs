using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    
    public virtual bool OnInteract(bool isTrue)
    {
        return isTrue;
    }
}
