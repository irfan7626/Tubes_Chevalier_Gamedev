using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject objectToActivate;  // Objek yang diaktifkan saat E ditekan
    public GameObject promptUI;          // UI yang muncul saat player dekat
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(!objectToActivate.activeSelf);
                Debug.Log("Interacted with object!");

                // Sembunyikan UI setelah interaksi
                if (promptUI != null)
                    promptUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}
