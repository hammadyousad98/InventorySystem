using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] itemSlots = new InventorySlot[20];
        public void Clear()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].DeleteItem();
            }
        }
    }
    [System.Serializable]
    public class InventorySlot
    {
        [System.NonSerialized]
        public InventoryManager parent;
        public ItemType[] allowedItems = new ItemType[0];
        public Item item;
        public int quantity;

        public InventorySlot()
        {
            item = null;
            quantity = 0;
        }
        public InventorySlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
        public void AddQuantity(int value)
        {
            quantity += value;
        }
        public void UpdateSlot(Item _item, int _quantity)
        {
            item = _item;
            quantity = _quantity;
        }
        public Itemobject ItemObject
        {
            get
            {
                if (item.Id >= 0)
                {
                    return parent.inventory.database.Items[item.Id];
                }
                return null;
            }
        }
        public bool CanPlaceInSlot(Itemobject itemObj)
        {
            if (allowedItems.Length <= 0 || itemObj == null || itemObj.itemData.Id<0)
                return true;
            for (int i = 0; i < allowedItems.Length; i++)
            {
                if (itemObj.itemType == allowedItems[i])
                    return true;
            }
            return false;
        }

        public void DeleteItem()
        {
            item = new Item();
            quantity = 0;
        }
    }
 
}