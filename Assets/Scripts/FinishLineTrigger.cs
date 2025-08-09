using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    // Seret objek 'TimerManager' (yang punya skrip MazeTimer) ke sini
    public MazeTimer mazeTimerScript;

    // Seret objek 'EndGamePanel' dari UI Anda ke sini
    public GameObject endGamePanel;

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang masuk adalah Player
        if (other.CompareTag("Player"))
        {
            // 1. Hentikan Timer
            if (mazeTimerScript != null)
            {
                mazeTimerScript.StopTimer();
            }

            // 2. Hentikan Gerakan Pemain
            // Ganti "Playermovement2" jika nama skrip gerakan Anda berbeda
            Playermovement2 playerMovement = other.GetComponent<Playermovement2>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // 3. Tampilkan Panel "The End"
            if (endGamePanel != null)
            {
                endGamePanel.SetActive(true);
            }
            
            // 4. Bebaskan dan tampilkan kursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 5. Nonaktifkan trigger ini agar tidak berjalan lagi
            gameObject.SetActive(false);
        }
    }
}