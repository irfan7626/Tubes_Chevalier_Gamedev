using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform inventoryPanel; // Diisi dengan Panel di Canvas
    public GameObject slotPrefab;    // Prefab slot (misalnya IconItem)
    public Sprite emptyIcon;         // Icon default jika tidak ada

    private List<string> keyList = new List<string>(); // Simpan ID kunci

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Menambahkan icon item visual
    public void AddItem(Sprite icon)
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);

        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        if (slot != null)
        {
            slot.SetItem(icon, "Item Tanpa Nama"); // Bisa diganti nama item sesuai data
        }
        else
        {
            Debug.LogWarning("InventorySlot script tidak ditemukan di prefab slot.");
        }
    }

    // Menambahkan kunci berdasar ID
    public void AddKey(string keyID)
    {
        if (!keyList.Contains(keyID))
        {
            keyList.Add(keyID);
            Debug.Log("Kunci " + keyID + " telah ditambahkan ke inventory.");
        }
    }

    // Mengecek apakah player punya kunci tertentu
    public bool HasKey(string keyID)
    {
        return keyList.Contains(keyID);
    }
}
