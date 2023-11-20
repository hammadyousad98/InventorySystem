using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public int Id;
        public ItemStats[] stats;

        public Item()
        {
            Name = "";
            Id = -1;
        }
        public Item(Itemobject item)
        {
            Name = item.name;
            Id = item.itemData.Id;
            stats = new ItemStats[item.itemData.stats.Length];
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = new ItemStats(item.itemData.stats[i].min, item.itemData.stats[i].max)
                {
                    attribute = item.itemData.stats[i].attribute
                };
            }
        }
    }
    [System.Serializable]
    public class ItemStats
    {
        public Attributes attribute;
        public int value;
        public int min;
        public int max;
        public ItemStats(int _min, int _max)
        {
            min = _min;
            max = _max;
            GenerateValue();
        }
        public void GenerateValue()
        {
            value = UnityEngine.Random.Range(min, max);
        }
    }

}