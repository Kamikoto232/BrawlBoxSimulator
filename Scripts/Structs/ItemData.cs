using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ItemData
{
    public enum Type { Brawler, BrawlerPower, Tickets, Gems, Coins }
    public Type ItemType;
    public Brawler BrawlerCard;
    public int Count;

    public ItemData(Type ItemType, int Count, Brawler BrawlerCard)
    {
        this.ItemType = ItemType;
        this.Count = Count;
        this.BrawlerCard = BrawlerCard;
    }
}
