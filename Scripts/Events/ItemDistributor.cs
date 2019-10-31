using UnityEngine;
using System.Collections;

public class ItemDistributor
{
    public delegate void GetItem(ItemData itemData);
    public static event GetItem OnGetItem;

    public static void AddItem(ItemData itemData)
    {
        OnGetItem(itemData);
    }
}
