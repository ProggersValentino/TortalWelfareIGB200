using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New General Template")]
public class GeneralMicroTaskTemplateSO : ScriptableObject
{
    [SerializeField] private MicroTaskSO mtData;
    
    public MicroTaskSO _mtData
    {
        get { return mtData; }
    }

    [SerializeField] private GameObject microTaskPref;
    
    public GameObject _microTaskPref
    {
        get { return microTaskPref; }
    }
}
