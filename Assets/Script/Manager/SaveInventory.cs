using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

namespace InventorySystem
{
    public class SaveInventory : MonoBehaviour
    {
        public delegate void SaveInventoryData(InventoryObject Container);
        public delegate void LoadInventoryData(InventoryObject Container);

        public static SaveInventoryData saveData;
        public static LoadInventoryData loadData;

        public string savePath;

        private void OnEnable()
        {
            saveData += Save;
            loadData += Load;
        }
        private void OnDisable()
        {
            saveData -= Save;
            loadData -= Load;
        }
        public void Save(InventoryObject Container)
        {
            Debug.Log("saving");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container.itemContainer);
            stream.Close();
        }
        public void Load(InventoryObject Container)
        {
            Debug.Log("Loading..");
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                Inventory newContainer = (Inventory)formatter.Deserialize(stream);
                Container.itemContainer=newContainer;
                for (int i = 0; i < Container.itemContainer.itemSlots.Length; i++)
                {
                    Container.itemContainer.itemSlots[i].UpdateSlot(newContainer.itemSlots[i].item, newContainer.itemSlots[i].quantity);
                }
                Debug.Log("Loaded..");
                stream.Close();
            }
        }
        public void Clear(InventoryObject Container)
        {
            Container.itemContainer = new Inventory();
        }
    }
}
