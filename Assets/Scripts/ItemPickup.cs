using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdParent; // assign an empty GameObject in front of the camera
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

        if (heldItem != null)
        {
            // Keep item at hold position
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