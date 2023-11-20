using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Database")]
    public class InventoryDatabase : ScriptableObject
    {
        public Itemobject[] Items;

        public void UpdateID()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].itemData.Id = i;
            }
        }
    }
}