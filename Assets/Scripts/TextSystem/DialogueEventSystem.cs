using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventSystem : MonoBehaviour
{
   public static event UnityAction<List<string>> StartDialogue;
   public static event UnityAction StopDialogue;

   public static void OnStartDialogue(List<string> value) => StartDialogue?.Invoke(value);
   public static void OnStopDialogue() => StopDialogue?.Invoke();
}
