using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bin : MonoBehaviour
{

    public UnityEvent execute;

    public SpriteRenderer renderer;
    
    public Sprite binOpenState;
    public Sprite binClosedState;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Food fod))
        {
            if(fod.isGettingPickedUp) fod.isOverBin = true; //are we getting picked up

            renderer.sprite = binOpenState;
            
            if (fod.isOverBin && !fod.isGettingPickedUp)
            {
                Destroy(fod.gameObject);
                renderer.sprite = binClosedState;
            }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Food fod))
        {
            renderer.sprite = binClosedState;
            fod.isOverBin = true;
        }
    }
}
