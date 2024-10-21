using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleFeederEventSystem : MonoBehaviour
{
    public static event Action<int> UpdateTurtleFedUI;
    public static event Action<int> UpdateAteRubbishUI;
    public static event Action<bool> GameIsProgressing;

    public static void OnGameIsProgressing(bool isGoing) => GameIsProgressing?.Invoke(isGoing);
    public static void OnUpdateTurtleFedUI(int increaseBy) => UpdateTurtleFedUI?.Invoke(increaseBy);
    public static void OnUpdateAteRubbishUI(int increaseBy) => UpdateAteRubbishUI?.Invoke(increaseBy);
}
