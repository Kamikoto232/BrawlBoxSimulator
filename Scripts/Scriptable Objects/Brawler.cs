using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Brawler")]
public class Brawler : ScriptableObject
{
    public Sprite Icon;
    public string BrawlerName;
    public enum Rarity { Normal, Rare, SuperRare, Epic, Mythical, Legendary }
    public Rarity RarityType;
}
