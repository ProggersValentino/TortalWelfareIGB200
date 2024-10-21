using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// the purpose of this script is to create and define audio clips that will be used throughout the game
/// </summary>
[CreateAssetMenu(menuName = "Audio/New Audio Data")]
public class AudioSO : ScriptableObject
{
   [SerializeField] private string lookupKey; //used for looking up the audio in the manager to play
   
   public string _lookupKey
   {
      get { return lookupKey; }
   }

   [SerializeField] private AudioMixerGroup mixerOutput; //where the audio will output from

   public AudioMixerGroup _mixerOutput
   {
      get { return mixerOutput; }
   }

   [SerializeField] private AudioClip audio; //the audio we want to play
   
   public AudioClip _audio
   {
      get { return audio; }
   }
}
