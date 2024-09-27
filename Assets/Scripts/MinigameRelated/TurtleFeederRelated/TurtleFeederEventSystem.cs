using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleFeederEventSystem : MonoBehaviour
{
    public static event Action UpdateTurtleFedUI;
    

    public static void OnUpdateTurtleFedUI() => UpdateTurtleFedUI?.Invoke();
}
