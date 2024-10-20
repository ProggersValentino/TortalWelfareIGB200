using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Video;

public class Torch : MonoBehaviour
{
   public float spotTimer;
   public float spotTime = 3f;
   private bool isLookingAtFox = false;

   public float currentTime;
   
   private void OnTriggerEnter(Collider other)
   {
      Debug.LogWarning(other.gameObject.name);

      if(other.gameObject.CompareTag("Fox"))
      {
         TimerEventManager.OnTimerStart(RaceTimer.TimerType.countdown);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if(other.gameObject.CompareTag("Fox"))
      {
        TimerEventManager.OnTimerStop();
        TimerEventManager.OnTimerReset();
      }
   }

   private void OnTriggerStay(Collider other)
   {
      
   }

   private void Update()
   {

      currentTime = Time.time;
      if (Time.time > spotTimer && isLookingAtFox)
      {
         Debug.LogWarning("we have spotted te fox");
      }
   }
}
