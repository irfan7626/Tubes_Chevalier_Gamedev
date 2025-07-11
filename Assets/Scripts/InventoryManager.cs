using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform inventoryPanel; // Isi dengan InventoryPanel dari Canvas
    public GameObject slotPrefab;    // Prefab slot icon (misal: IconItem prefab)
    public Sprite emptyIcon;         // Icon kosong (optional)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(Sprite icon)
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);

        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        if (slot != null)
        {
            slot.SetItem(icon, "Item Tanpa Nama"); // Ganti dengan nama item yang relevan kalau ada
        }
        else
        {
            Debug.LogWarning("InventorySlot script tidak ditemukan di prefab slot.");
        }
    }
}
