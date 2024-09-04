using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// the purpose of this script is to communicate to game elements that need to update the
/// difficulty of the game everytime the level shifts 
/// </summary>
public class DifficultyEventSystem : MonoBehaviour
{
    public static event UnityAction UpdateDifficulty;

    public static void OnUpdateDifficulty() => UpdateDifficulty?.Invoke();
}
