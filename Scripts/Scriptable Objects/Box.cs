using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Box")]
public class Box : ScriptableObject
{
    public BoxItemData[] Items;
    public int MinItems, MaxItems, Cost;
    public enum CostType { Coins, Gems, ADS, Free };
    public CostType CostMethod;
    public Sprite BoxSprite;
}
