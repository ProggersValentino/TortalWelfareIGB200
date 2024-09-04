using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// the purpose of this is to act as a carrier of data between scenes 
/// </summary>
[CreateAssetMenu(menuName = "DataBank/New Difficulty Data")]
public class DifficultManagerSO : ScriptableObject
{
    [SerializeField] private float difficultyMeter;
    
   public float _difficultyMeter
       {
           get { return difficultyMeter; }
           set { difficultyMeter = value ; }
       }
 
    //public UnityEvent ChangeDiffBar;

    /// <summary>
    /// to calculate the new difficulty metre value
    /// </summary>
    /// <param name="difference"></param>
    public void CalculateDifference(float difference)
    {
        Debug.LogWarning("we calculated");
        
        _difficultyMeter += difference;
    }
    
}
