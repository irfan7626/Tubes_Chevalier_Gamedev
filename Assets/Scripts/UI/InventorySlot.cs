using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public string itemID;

    public void SetItem(Sprite icon, string id)
    {
        if (iconImage != null)
        {
            iconImage.sprite = icon;
            iconImage.enabled = true;
        }
        itemID = id;
    }
}