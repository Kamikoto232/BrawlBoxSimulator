using UnityEngine;
using System.Collections;

[System.Serializable]
public struct BoxItemData
{
    public ItemData.Type ItemType;
    [Range(0,1)]
    public float Chance;
    public int CountMin;
    public int CountMax;
}
