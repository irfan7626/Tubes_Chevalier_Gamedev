using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdParent;
    private GameObject heldItem;
    private Rigidbody heldItemRb;

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

        if (heldItem != null && heldItem.transform.parent == holdParent)
        {
            heldItem.transform.position = holdParent.position;
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;
        // Tembakkan "laser" dari tengah kamera ke depan
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
        {
            // --- BAGIAN BARU: Cek apakah yang dilihat adalah papan yang bisa dihancurkan ---
            BreakableObject breakable = hit.transform.GetComponent<BreakableObject>();
            if (breakable != null)
            {
                // Jika ya, panggil fungsi untuk mencoba menghancurkannya
                breakable.AttemptToBreak();
                return; // Hentikan fungsi di sini agar tidak mencoba pickup item lain
            }
            // --------------------------------------------------------------------------

            // Kode lama Anda untuk pickup kunci (sudah benar)
            KeyItem key = hit.transform.GetComponent<KeyItem>();
            if (key != null)
            {
                InventoryManager.Instance.AddKey(key.keyID, key.keyIcon);
                Destroy(hit.transform.gameObject);
            }
            // Kode lama Anda untuk memegang objek fisik (sudah benar)
            else if (hit.transform.CompareTag("Pickup"))
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
            }
        }
    }

    void DropItem()
    {
        // --- PERUBAHAN: Logika untuk menghapus kunci saat dijatuhkan ---
        if (heldItem != null)
        {
            KeyItem key = heldItem.GetComponent<KeyItem>();
            if (key != null)
            {
                InventoryManager.Instance.RemoveKey(key.keyID);
            }

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
}