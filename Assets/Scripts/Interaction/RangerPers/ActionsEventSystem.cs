using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the purpose of this script is to provide direct utility to commincate between MicroTask class and ActionSpawnerMan
/// </summary>
public class ActionsEventSystem : MonoBehaviour
{
    public static event Func<List<GameObject>> RetrieveMicroTasks;
    public static event UnityAction<string> DeleteMicroTaskFromPersistent;
    public static event UnityAction<int> InitiateInjection;
    public static event UnityAction SendReadySignal;

    public static List<GameObject> OnRetrieveMicroTasks() => RetrieveMicroTasks?.Invoke();
    public static void OnDeleteFromPersistent(string UID) => DeleteMicroTaskFromPersistent?.Invoke(UID);
    public static void OnInitiateInjection(int numberToInject) => InitiateInjection?.Invoke(numberToInject);
    public static void OnSendReadySignal() => SendReadySignal?.Invoke();
}
