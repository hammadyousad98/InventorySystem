using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace InventorySystem
{
    public class Player : MonoBehaviour
    {
        public InventoryObject defaultInventory;
        public InventoryObject equipmentInventory;

        public TextMeshProUGUI inventoryText;
        private void Awake()
        {
            defaultInventory.database.UpdateID();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                GroundItem item = other.GetComponent<GroundItem>();
                Item _item = new Item(item.item);

                bool isAdded = defaultInventory.AddItem(_item, 1);
                if(isAdded)
                {
                    InventoryManager.updateInventory?.Invoke();
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log("Inventory Full");
                }
            }
        }
        IEnumerator DisplayText()
        {
            inventoryText.text = "InventoryFull";
            yield return new WaitForSeconds(2.0f);
            inventoryText.text = "";
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SaveInventory.saveData?.Invoke(defaultInventory);
                //SaveInventory.saveData?.Invoke(equipmentInventory);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SaveInventory.loadData?.Invoke(defaultInventory);
               // SaveInventory.loadData?.Invoke(equipmentInventory);
                InventoryManager.updateInventory?.Invoke();
            }
        }
        private void OnApplicationQuit()
        {
            /*defaultInventory.itemContainer.itemSlots = new InventorySlot[20];
            equipmentInventory.itemContainer.itemSlots = new InventorySlot[6];*/
        }
    }
}