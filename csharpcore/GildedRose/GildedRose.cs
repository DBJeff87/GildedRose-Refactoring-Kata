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
            //if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            //{
            //    if (item.Quality > 0)
            //    {
            //        if (item.Name == "Conjured")
            //        {
            //            item.Quality -= item.Quality > 1 ? 2: 1;
            //        }
            //        else if (item.Name != "Sulfuras, Hand of Ragnaros")
            //        {
            //            item.Quality = item.Quality - 1;
            //        }
            //    }
            //}
            //else
            //{
            //    if (item.Quality < 50)
            //    {
            //        item.Quality = item.Quality + 1;

            //        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            //        {
            //            if (item.SellIn < 11)
            //            {
            //                if (item.Quality < 50)
            //                {
            //                    item.Quality = item.Quality + 1;
            //                }
            //            }

            //            if (item.SellIn < 6)
            //            {
            //                if (item.Quality < 50)
            //                {
            //                    item.Quality = item.Quality + 1;
            //                }
            //            }
            //        }
            //    }
            //}

            this.UpdateSellIn(item);
            this.UpdateItemQuality(item);

            //if (item.SellIn < 0)
            //{
            //    if (item.Name != "Aged Brie")
            //    {
            //        if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
            //        {
            //            if (item.Quality > 0)
            //            {
            //                if (item.Name == "Conjured")
            //                {
            //                    item.Quality -= item.Quality > 1 ? 2 : 1;
            //                }
            //                else if (item.Name != "Sulfuras, Hand of Ragnaros")
            //                {
            //                    item.Quality = item.Quality - 1;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            item.Quality = item.Quality - item.Quality;
            //        }
            //    }
            //    else
            //    {
            //        if (item.Quality < 50)
            //        {
            //            item.Quality = item.Quality + 1;
            //        }
            //    }
            //}
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

    private int GetAllowedQuality(int quality)
    {
        if (quality < 0) return 0;
        if (quality > 50) return 50;

        return quality;
    }
}