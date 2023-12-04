using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    public void Item_SellIn_And_Quality_Should_Degrade_Each_Night()
    {
        var items = new List<Item> { new Item { Name = "Staff of Refactoring", SellIn = 8, Quality = 10 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(7, items[0].SellIn);
        Assert.AreEqual(9, items[0].Quality);
    }

    [Test]
    public void Item_Quality_Should_Degrade_Twice_As_Fast_Once_Sell_By_Date_Has_Passed()
    {
        var items = new List<Item> { new Item { Name = "Amulet of Code", SellIn = 0, Quality = 10 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(8, items[0].Quality);
    }

    [Test]
    [TestCase("Potion of Enormousness")]
    [TestCase(GildedRose.AGED_BRIE)]
    [TestCase(GildedRose.BACKSTAGE_PASSES)]
    [TestCase(GildedRose.SULFURAS)]
    [TestCase(GildedRose.CONJURED)]
    public void Quality_Should_Never_Be_Negative(string itemName)
    {
        var items = new List<Item> { new Item { Name = itemName, SellIn = 0, Quality = 1 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality >= 0);
    }

    [Test]
    [TestCase("Potion of Bravado")]
    [TestCase(GildedRose.AGED_BRIE)]
    [TestCase(GildedRose.BACKSTAGE_PASSES)]
    [TestCase(GildedRose.SULFURAS)]
    [TestCase(GildedRose.CONJURED)]
    public void Quality_Should_Never_Increase_To_Over_50(string itemName)
    {
        var items = new List<Item> { new Item { Name = itemName, SellIn = 10, Quality = 49 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality <= 50);
    }

    [Test]
    public void Aged_Brie_Quality_Should_Increase_Over_Time()
    {
        var items = new List<Item> { new Item { Name = GildedRose.AGED_BRIE, SellIn = 4, Quality = 17 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(18, items[0].Quality);
    }

    [Test]
    public void Aged_Brie_Quality_Should_Increase_Twice_As_Fast_Once_Sell_By_Date_Has_Passed()
    {
        var items = new List<Item> { new Item { Name = GildedRose.AGED_BRIE, SellIn = 0, Quality = 24 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(26, items[0].Quality);
    }

    [Test]
    public void Sulfuras_SellIn_And_Quality_Should_Never_Change()
    {
        var items = new List<Item> { new Item { Name = GildedRose.SULFURAS, SellIn = 5, Quality = 112 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(5, items[0].SellIn);
        Assert.AreEqual(112, items[0].Quality);
    }

    [Test]
    public void Backstage_Passes_Quality_Should_Increase_Over_Time()
    {
        var items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE_PASSES, SellIn = 14, Quality = 17 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(18, items[0].Quality);
    }

    [Test]
    public void Backstage_Passes_Quality_Should_Increase_Twice_As_Fast_When_SellIn_Is_Between_6_And_10()
    {
        //Try at 10 days.
        var items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE_PASSES, SellIn = 10, Quality = 14 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(16, items[0].Quality);

        //Fast-forward to 6 days.
        items[0].SellIn = 6;
        items[0].Quality = 22;
        app.UpdateQuality();
        Assert.AreEqual(24, items[0].Quality);
    }

    [Test]
    public void Backstage_Passes_Quality_Should_Increase_Thrice_As_Fast_When_SellIn_Is_Between_1_And_5()
    {
        //Try at 5 days.
        var items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE_PASSES, SellIn = 5, Quality = 24 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(27, items[0].Quality);

        //Fast-forward to 1 day.
        items[0].SellIn = 1;
        items[0].Quality = 36;
        app.UpdateQuality();
        Assert.AreEqual(39, items[0].Quality);
    }

    [Test]
    public void Backstage_Passes_Quality_Should_Decrease_To_Zero_Once_The_Concert_Has_Passed()
    {
        var items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE_PASSES, SellIn = 0, Quality = 39 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].Quality);
    }

    [Test]
    public void Conjured_Items_Quality_Should_Degrade_Twice_As_Fast()
    {
        var items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 8, Quality = 10 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(8, items[0].Quality);
    }

    [Test]
    public void Conjured_Items_Quality_Should_Degrade_Four_Times_As_Fast_Once_Sell_By_Date_Has_Passed()
    {
        var items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 0, Quality = 14 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(10, items[0].Quality);
    }
}