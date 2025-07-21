using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform inventoryPanel;
    public GameObject slotPrefab;
    private List<string> keyList = new List<string>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void AddKey(string keyID, Sprite keyIcon)
    {
        if (!keyList.Contains(keyID))
        {
            keyList.Add(keyID);
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlot slot = newSlot.GetComponent<InventorySlot>();
            if (slot != null) { slot.SetItem(keyIcon, keyID); }
        }
    }

    public void RemoveKey(string keyID)
    {
        if (keyList.Contains(keyID))
        {
            keyList.Remove(keyID);
            foreach (Transform slotTransform in inventoryPanel)
            {
                InventorySlot slot = slotTransform.GetComponent<InventorySlot>();
                if (slot != null && slot.itemID == keyID)
                {
                    Destroy(slot.gameObject);
                    break;
                }
            }
        }
    }

    public bool HasKey(string keyID)
    {
        return keyList.Contains(keyID);
    }
}