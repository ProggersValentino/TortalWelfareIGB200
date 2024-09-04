using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New Item Data")]
public class ItemSO : ScriptableObject, IItem
{
    [SerializeField] private int itemID;
    public int _itemID { get; set; }

    public enum ItemType
    {
        Clothing, Furniture, Keys, Action
    }

    [SerializeField] private ItemType itemType;
    public ItemType _itemType { get { return itemType; } }
}
