using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    public const string AGED_BRIE = "Aged Brie";
    public const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    public const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    public const string CONJURED = "Conjured";

    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in this._items)
        {
            this.UpdateSellIn(item);
            this.UpdateItemQuality(item);
        }
    }
    private void UpdateSellIn(Item item)
    {
        item.SellIn -= item.Name == SULFURAS ? 0 : 1;
    }

    private void UpdateItemQuality(Item item)
    {
        int changeInQuality;

        switch (item.Name)
        {
            case SULFURAS:
                return;
            case AGED_BRIE:
                changeInQuality = item.SellIn < 0 ? 2 : 1;
                break;
            case BACKSTAGE_PASSES:
                if (item.SellIn < 0)
                {
                    changeInQuality = -item.Quality;
                }
                else if (item.SellIn < 5)
                {
                    changeInQuality = 3;
                }
                else if (item.SellIn < 10)
                {
                    changeInQuality = 2;
                }
                else
                {
                    changeInQuality = 1;
                }
                break;
            case CONJURED:
                changeInQuality = item.SellIn < 0 ? -4 : -2;
                break;
            default:
                changeInQuality = item.SellIn < 0 ? -2 : -1;
                break;
        }

        item.Quality = this.GetAllowedQuality(item.Quality + changeInQuality);
    }

    //This method should never be called for Sulfuras, since it can have a quality above 50.
    private int GetAllowedQuality(int quality)
    {
        if (quality < 0) return 0;
        if (quality > 50) return 50;

        return quality;
    }
}