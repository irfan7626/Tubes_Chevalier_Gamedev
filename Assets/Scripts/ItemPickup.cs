using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdParent; // GameObject kosong di depan kamera
    private GameObject heldItem;
    private Rigidbody heldItemRb;

    [Header("Item Info")]
    public Sprite itemIcon;
    public bool isPuzzleItem = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                TryPickupItem();
            }
            else
            {
                DropItem();
            }
        }

        if (heldItem != null)
        {
            heldItem.transform.position = holdParent.position;
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                heldItem = hit.transform.gameObject;
                heldItemRb = heldItem.GetComponent<Rigidbody>();

                if (heldItemRb != null)
                {
                    heldItemRb.useGravity = false;
                    heldItemRb.isKinematic = true;
                }

                heldItem.transform.position = holdParent.position;
                heldItem.transform.SetParent(holdParent);

                // PERBAIKAN: Ambil data dari item yang di-pick up, bukan dari script ini
                ItemPickup itemScript = hit.transform.GetComponent<ItemPickup>();
                if (itemScript != null && itemScript.isPuzzleItem && itemScript.itemIcon != null)
                {
                    InventoryManager.Instance.AddItem(itemScript.itemIcon);
                }
            }
        }
    }

    void DropItem()
    {
        if (heldItemRb != null)
        {
            heldItemRb.useGravity = true;
            heldItemRb.isKinematic = false;
        }

        heldItem.transform.SetParent(null);
        heldItem = null;
        heldItemRb = null;
    }
}
