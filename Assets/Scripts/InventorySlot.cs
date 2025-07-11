using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;         // Di-drag manual dari child Image
    public string itemName;

    // Set dari InventoryManager
    public void SetItem(Sprite newIcon, string name)
    {
        icon.sprite = newIcon;
        itemName = name;
    }

    // Dipanggil saat diklik
    public void OnClickSlot()
    {
        Debug.Log("Klik item: " + itemName);
    }
}
