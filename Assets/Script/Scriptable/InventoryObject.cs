using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public InventoryDatabase database;
        public Inventory itemContainer;
        public bool AddItem(Item _item, int _quantity)
        {
            if (TotalEmptySlot() <= 0)
            {
                return false;
            }
            InventorySlot slot = FindIteminInventory(_item);
            Debug.Log("Database Length " + database.Items[_item.Id]);
            if (!database.Items[_item.Id].stackAble || slot == null)
            {
                return SetSlot(_item, _quantity); 
            }
            slot.AddQuantity(_quantity);
            return true;
        }

        int TotalEmptySlot()
        {
            int count=0;
            foreach(var slots in itemContainer.itemSlots)
            {
                count++;
            }
            return count;
        }
        InventorySlot FindEmptySlot()
        {
            for (int i = 0; i < itemContainer.itemSlots.Length; i++)
            {
                if (itemContainer.itemSlots[i].item.Id <= -1)
                {
                    return itemContainer.itemSlots[i];
                }
            }
            return null;
        }
        InventorySlot FindIteminInventory(Item _item)
        {
            for (int i = 0; i < itemContainer.itemSlots.Length; i++)
            {
                if (itemContainer.itemSlots[i].item.Id == _item.Id)
                {
                    return itemContainer.itemSlots[i];
                }
            }
            return null;
        }
        bool SetSlot(Item _item, int _quantity)
        {
            InventorySlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                emptySlot.UpdateSlot( _item, _quantity);
                return true;
            }
            else return false;
        }

        public void MoveItem(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.quantity);
                item2.UpdateSlot(item1.item, item1.quantity);
                item1.UpdateSlot(temp.item, temp.quantity);
            }
        }

        public void RemoveItem(Item _item)
        {
            Debug.Log("removing 2");
            for (int i = 0; i < itemContainer.itemSlots.Length; i++)
            {
                if (itemContainer.itemSlots[i].item == _item)
                {
                    itemContainer.itemSlots[i].UpdateSlot( null, 0);
                }
            }
        }

    }
}