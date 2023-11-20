﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

namespace InventorySystem
{
    public class NormalInventoryManager : InventoryManager
    {
        public GameObject inventoryPrefab;

        public override void CreateSlots()
        {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

            for (int i = 0; i < inventory.itemContainer.itemSlots.Length; i++)
            {
                currentSlot = inventory.itemContainer.itemSlots[i];
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

                // SetSlotValues(currentSlot, obj, null, new Color(1, 1, 1, 0), "");
                if (currentSlot.item.Id >= 0)
                {
                    SetSlotValue(obj,
                    currentSlot.ItemObject.itemSprite,
                    new Color(1, 1, 1, 1),
                     currentSlot.quantity == 1 ? "" : currentSlot.quantity.ToString("n0"));
                }
                else
                {
                    SetSlotValue(obj, null, new Color(1, 1, 1, 0), "");
                }

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                itemsDisplayed.Add(obj, currentSlot);
            }
        }
    }
}