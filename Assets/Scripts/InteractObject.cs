using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject objectToActivate; // Objek yang akan diaktifkan/dinonaktifkan
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(!objectToActivate.activeSelf); // Toggle ON/OFF

                // Sembunyikan UI setelah interaksi
                InteractionUI.Instance.ShowPrompt(false);

                // Reset agar interaksi tidak bisa diulang terus-menerus
                isPlayerNear = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            InteractionUI.Instance.ShowPrompt(true); // Tampilkan UI interaksi
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            InteractionUI.Instance.ShowPrompt(false); // Sembunyikan UI saat keluar
        }
    }
}
