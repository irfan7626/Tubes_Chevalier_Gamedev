using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float pickupRange = 3f;
    public Transform holdParent;

    [Header("Audio Feedback")]
    public AudioClip keyPickupSound; // Seret suara pickup kunci ke sini

    private GameObject heldItem;
    private Rigidbody heldItemRb;
    private AudioSource audioSource;

    void Start()
    {
        // Dapatkan komponen AudioSource saat game dimulai
        audioSource = GetComponent<AudioSource>();
    }

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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
        {
            // Cek untuk papan yang bisa dihancurkan
            BreakableObject breakable = hit.transform.GetComponent<BreakableObject>();
            if (breakable != null)
            {
                breakable.AttemptToBreak();
                return;
            }

            // Cek untuk kunci
            KeyItem key = hit.transform.GetComponent<KeyItem>();
            if (key != null)
            {
                // --- BAGIAN BARU: Mainkan suara pickup ---
                if (keyPickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(keyPickupSound);
                }
                // ----------------------------------------

                // Tambahkan kunci ke inventory dan hancurkan objeknya
                InventoryManager.Instance.AddKey(key.keyID, key.keyIcon);
                Destroy(hit.transform.gameObject);
            }
            // Cek untuk objek fisik lain yang bisa dipegang
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