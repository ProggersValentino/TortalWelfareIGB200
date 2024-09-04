using System.Collections;
using System.Collections.Generic;using System.Threading;
using UnityEngine;

/// <summary>
/// to set up the structure of items for our inventory to detect and recieve items
/// </summary>
public interface IItem
{
    public int _itemID { get; set; }
}
