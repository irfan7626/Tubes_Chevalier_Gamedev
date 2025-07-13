using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string requiredKeyID = "KeyA"; // Harus cocok dengan keyID dari KeyItem
    public GameObject doorClosed;
    public GameObject doorOpened;
    public GameObject interactionUI;
    public AudioSource openSound;

    private bool isPlayerNear = false;
    private bool doorIsOpen = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryManager.Instance.HasKey(requiredKeyID) && !doorIsOpen)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("Kunci tidak ditemukan di inventory.");
            }
        }
    }

    private void OpenDoor()
    {
        if (doorClosed != null) doorClosed.SetActive(false);
        if (doorOpened != null) doorOpened.SetActive(true);
        if (interactionUI != null) interactionUI.SetActive(false);

        if (openSound != null) openSound.Play();

        doorIsOpen = true;
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
