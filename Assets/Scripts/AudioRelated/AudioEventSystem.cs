using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// the purpose of this script is act as a utility script for the audio man being able to access and play audio from virtually anywhere
/// </summary>
public class AudioEventSystem : MonoBehaviour
{
    public static event Action<string> PlayAudio;
    public static event Action<string> StopAudio;

    public static void OnPlayAudio(string searchKey) => PlayAudio?.Invoke(searchKey);
    public static void OnStopAudio(string searchKey) => StopAudio?.Invoke(searchKey);
}
