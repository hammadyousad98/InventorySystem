using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item" , menuName = "Inventory System/Item")]
    public class Itemobject : ScriptableObject
    {
        public bool stackAble;
        public Sprite itemSprite;
        public ItemType itemType;
        public string description;
        public Item itemData = new Item();

        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
    }
}
