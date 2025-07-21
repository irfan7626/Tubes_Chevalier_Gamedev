using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject puzzleUI;          // Seret PuzzleBoard ke sini
    public GameObject promptUI;          // Seret UI "Tekan E" ke sini
    private bool isPlayerNear = false;

    void Update()
    {
        // Hanya jalankan jika pemain dekat DAN menekan E
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ActivatePuzzle();
        }
    }

    void ActivatePuzzle()
    {
        if (puzzleUI != null)
        {
            // 1. Aktifkan UI Puzzle
            puzzleUI.SetActive(true);

            // 2. Tampilkan kursor mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 3. Matikan gerakan pemain
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Ganti "PlayerMovement2" dengan nama skrip gerakanmu jika berbeda
                var playerMovement = player.GetComponent<Playermovement2>();
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }
            }

            // Sembunyikan UI prompt "Tekan E"
            if (promptUI != null)
                promptUI.SetActive(false);
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