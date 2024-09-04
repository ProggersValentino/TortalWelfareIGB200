using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New Inventory Data")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private float gold;

    public List<ItemSO> items;

}
