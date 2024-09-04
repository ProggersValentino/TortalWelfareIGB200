using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DataBank/Player Data")]
public class PlayerSO : ScriptableObject
{

     [SerializeField] private Vector3 lastPlayerLocoBeforeSceneChange;
    public Vector3 _lastPlayerLocoBeforeSceneChange {get { return lastPlayerLocoBeforeSceneChange;} set { lastPlayerLocoBeforeSceneChange = value; } }

    [SerializeField] private InventorySO inventory;
    public InventorySO _inventory { get { return inventory; } }
    
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
