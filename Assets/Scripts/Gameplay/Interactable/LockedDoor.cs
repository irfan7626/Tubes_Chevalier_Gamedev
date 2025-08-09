using UnityEngine;
using System.Collections.Generic;

public class LockedDoor : MonoBehaviour
{
    [Header("Door Models")]
    public GameObject doorClosed;
    public GameObject doorOpened;

    [Header("Lock Settings")]
    public bool isLocked;
    public List<string> requiredKeyIDs = new List<string>();

    [Header("UI & Sound")]
    public GameObject interactionUI;
    public AudioSource openSound;

    private bool isPlayerNear = false;
    private bool doorIsOpen = false;

    void Update()
    {
        if (isPlayerNear && !doorIsOpen && Input.GetKeyDown(KeyCode.E))
        {
            // --- INI TES PALING PENTING ---
            Debug.Log("======================================");
            Debug.Log("Tombol E Ditekan pada Pintu: " + gameObject.name);
            Debug.Log("Status 'isLocked' di Inspector adalah: " + isLocked);
            // ------------------------------------

            if (isLocked)
            {
                if (CheckForAllKeys())
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("GAGAL: Kunci tidak lengkap.");
                }
            }
            else
            {
                Debug.Log("Pintu ini tidak terkunci, membuka...");
                OpenDoor();
            }
        }
    }

    private bool CheckForAllKeys()
    {
        // Jika tidak ada kunci yang dibutuhkan, anggap saja gagal.
        if (requiredKeyIDs.Count == 0)
        {
            Debug.LogWarning("GAGAL: Daftar kunci yang dibutuhkan kosong!");
            return false;
        }

        foreach (string keyID in requiredKeyIDs)
        {
            // Cek apakah inventory punya kunci ini
            if (!InventoryManager.Instance.HasKey(keyID))
            {
                Debug.LogError("GAGAL: Kunci '" + keyID + "' TIDAK ADA di inventory!");
                return false; // Langsung hentikan jika satu kunci saja tidak ada
            }
        }

        Debug.Log("SUKSES: Semua kunci berhasil ditemukan!");
        return true;
    }

    private void OpenDoor()
    {
        doorIsOpen = true;

        if (isLocked)
        {
            foreach (string keyID in requiredKeyIDs)
            {
                InventoryManager.Instance.RemoveKey(keyID);
            }
        }
        
        if (doorClosed != null) doorClosed.SetActive(false);
        if (doorOpened != null) doorOpened.SetActive(true);
        if (interactionUI != null) interactionUI.SetActive(false);
        if (openSound != null) openSound.Play();

        GetComponent<Collider>().enabled = false;
    }

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