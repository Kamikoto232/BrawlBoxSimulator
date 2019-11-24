using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrawlersManager : MonoBehaviour
{
    public static BrawlersManager instance;
    public List<Brawler> Brawlers = new List<Brawler>();
    private float luckyFactor;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ItemDistributor.OnGetItem += ChangeLucky;
    }

    private void ChangeLucky(ItemData itemData)
    {
        if (itemData.ItemType == ItemData.Type.Brawler)
        {
            switch (itemData.BrawlerCard.RarityType)
            {
                case Brawler.Rarity.Normal:
                    luckyFactor -= 0.001f;
                    break;
                case Brawler.Rarity.Rare:
                    luckyFactor -= 0.002f;
                    break;
                case Brawler.Rarity.SuperRare:
                    luckyFactor -= 0.003f;
                    break;
                case Brawler.Rarity.Epic:
                    luckyFactor -= 0.005f;
                    break;
                case Brawler.Rarity.Mythical:
                    luckyFactor -= 0.008f;
                    break;
                case Brawler.Rarity.Legendary:
                    luckyFactor -= 0.01f;
                    break;
            }
        }
        else
            luckyFactor += 0.001f;
    }

    public static float GetChance()
    {
        float chance = instance.luckyFactor;

        

        return chance;
    }

    public static Brawler GetBrawler()
    {
        float chance = instance.luckyFactor;
        Brawler brawler = null;

        chance += Random.value;

        if (chance > 1f)
            brawler = GetRandomBrawler(Brawler.Rarity.Normal);
        if (chance > 1.4f)
            brawler = GetRandomBrawler(Brawler.Rarity.Rare);
        if (chance > 1.6f)
            brawler = GetRandomBrawler(Brawler.Rarity.SuperRare);
        if (chance > 1.8f)
            brawler = GetRandomBrawler(Brawler.Rarity.Epic);
        if (chance > 1.9f)
            brawler = GetRandomBrawler(Brawler.Rarity.Mythical);
        if (chance > 2)
            brawler = GetRandomBrawler(Brawler.Rarity.Legendary);

        return brawler;
    }

    private static Brawler GetRandomBrawler(Brawler.Rarity rarity)
    {
        Brawler brawler = null;
        Brawler[] brawlersSorted;

        brawlersSorted = instance.Brawlers.FindAll(x => x.RarityType == rarity).ToArray();
        brawler = brawlersSorted[Random.Range(0, brawlersSorted.Length)];

        return brawler;
    }
}
