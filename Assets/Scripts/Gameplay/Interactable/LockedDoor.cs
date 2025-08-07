using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("Door Models")]
    public GameObject doorClosed; // Model pintu tertutup
    public GameObject doorOpened; // Model pintu terbuka

    [Header("Lock Settings")]
    public bool isLocked; // <-- CENTANG INI JIKA PINTU TERKUNCI
    public string requiredKeyID; // ID kunci jika isLocked = true

    [Header("UI & Sound")]
    public GameObject interactionUI;
    public AudioSource openSound;

    private bool isPlayerNear = false;
    private bool doorIsOpen = false;

    void Update()
    {
        if (isPlayerNear && !doorIsOpen && Input.GetKeyDown(KeyCode.E))
        {
            // Cek apakah pintu ini tipenya terkunci
            if (isLocked)
            {
                // Jika terkunci, cek apakah pemain punya kunci
                if (InventoryManager.Instance.HasKey(requiredKeyID))
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("Pintu ini terkunci. Butuh kunci: " + requiredKeyID);
                }
            }
            else
            {
                // Jika tidak terkunci, langsung buka pintu
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        doorIsOpen = true;

        // Hapus kunci dari inventory HANYA jika pintu tadinya terkunci
        if (isLocked)
        {
            InventoryManager.Instance.RemoveKey(requiredKeyID);
        }

        // Logika lama Anda untuk menukar model pintu (sudah benar)
        if (doorClosed != null) doorClosed.SetActive(false);
        if (doorOpened != null) doorOpened.SetActive(true);
        if (interactionUI != null) interactionUI.SetActive(false);
        if (openSound != null) openSound.Play();

        // Nonaktifkan collider agar tidak bisa diinteraksi lagi
        GetComponent<Collider>().enabled = false;
    }

    // Bagian OnTriggerEnter dan OnTriggerExit tetap sama seperti milik Anda
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!doorIsOpen && interactionUI != null)
                interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionUI != null)
                interactionUI.SetActive(false);
        }
    }
}