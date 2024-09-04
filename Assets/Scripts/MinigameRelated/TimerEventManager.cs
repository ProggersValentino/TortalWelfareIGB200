using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// becasue this is static we dont need a direct reference to it thus enabling us to add listeners to
/// these actions
/// </summary>
public static class TimerEventManager
{
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction<float> TimerUpdate;

    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerStop() => TimerStop?.Invoke();
    
    public static void OnTimerUpdate(float value) => TimerUpdate?.Invoke(value);

}
