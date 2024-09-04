using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranger GM Data", menuName = "DataBank/New RGM Data")]
public class RangerGameManagerSO : ScriptableObject
{
   [SerializeField] private List<RangerData> rangers;
   
   
}
