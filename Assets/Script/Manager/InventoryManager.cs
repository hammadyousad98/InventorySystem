using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace InventorySystem
{
    public abstract class InventoryManager : MonoBehaviour
    {
        public delegate void UpdateInventory();
        public static UpdateInventory updateInventory;

        public InventoryObject inventory;

        protected InventorySlot currentSlot;

        protected Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        #region Unity BuiltIn Functions
        private void Start()
        {
            for (int i = 0; i < inventory.itemContainer.itemSlots. Length; i++)
            {
                inventory.itemContainer.itemSlots[i].parent = this;
                //inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
            }
            CreateSlots();

            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        }
 
        private void OnEnable()
        {
            updateInventory += UpdateAllSlots;
        }

        private void OnDisable()
        {
            updateInventory -= UpdateAllSlots;
        }
        #endregion

        #region Slots Creation and Updation
        public abstract void CreateSlots();

        public void UpdateAllSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> slot in itemsDisplayed)
            {
                if (slot.Value.item.Id >= 0)
                {
                    SetSlotValue(slot.Key,
                        slot.Value.ItemObject.itemSprite,
                        new Color(1, 1, 1, 1), slot.Value.quantity == 1 ? "" : slot.Value.quantity.ToString("n0")
                        );
                    
                }
                else
                {
                    SetSlotValue(slot.Key, null, new Color(1, 1, 1, 0), "");
                }
            }
        }
        public void SetSlotValue(GameObject obj, Sprite spr, Color clr, string val)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = spr;
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = clr;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = val;
        }

        #endregion

        #region Events
        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.itemHoverObj = obj;
        }
        public void OnExit(GameObject obj)
        {
            MouseData.itemHoverObj = null;
        }
        public void OnEnterInterface(GameObject obj)
        {
            MouseData.manager = obj.GetComponent<InventoryManager>();
        }
        public void OnExitInterface(GameObject obj)
        {
            MouseData.manager = null;
        }
        public void OnDragStart(GameObject obj)
        {
            if (itemsDisplayed[obj].item.Id >= 0)
            {
                GameObject mouseObject = new GameObject();
                mouseObject.transform.SetParent(transform.parent);
                var rt = mouseObject.AddComponent<RectTransform>();
                var img = mouseObject.AddComponent<Image>();
                rt.sizeDelta = new Vector2(100, 100);
                img.sprite = itemsDisplayed[obj].ItemObject.itemSprite;
                img.raycastTarget = false;
                MouseData.itemDraggedobj = mouseObject;
            }
        }
        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.itemDraggedobj);
            if (MouseData.manager == null)
            {
                itemsDisplayed[obj].DeleteItem();
                updateInventory?.Invoke();
                return;
            }
            if (MouseData.itemHoverObj)
            {
                InventorySlot mouseHoverSlot = MouseData.manager.itemsDisplayed[MouseData.itemHoverObj];
                inventory.MoveItem(itemsDisplayed[obj], mouseHoverSlot);
            }
            updateInventory?.Invoke();
        }
        public void OnDrag(GameObject obj)
        {
            if (MouseData.itemDraggedobj != null)
                MouseData.itemDraggedobj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        #endregion
    }
}