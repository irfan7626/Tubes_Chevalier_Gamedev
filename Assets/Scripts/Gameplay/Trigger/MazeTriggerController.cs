using UnityEngine;

public class MazeTriggerController : MonoBehaviour
{
    // Referensi ke skrip timer
    public MazeTimer mazeTimerScript;

    // DIUBAH: Tambahkan variabel baru untuk menampung UI Timer
    public GameObject timerUIObject; // Seret objek UI Timer (misal: TimerPanel) ke sini

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Terkena! Objek yang menyentuh adalah: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Tag 'Player' terdeteksi. Mencoba memulai timer...");

            // DIUBAH: Aktifkan/munculkan UI Timer
            if (timerUIObject != null)
            {
                timerUIObject.SetActive(true);
            }

            // Panggil fungsi untuk memulai timer (ini sudah benar)
            if (mazeTimerScript != null)
            {
                mazeTimerScript.StartTimer();
            }
            
            // Hancurkan objek trigger ini agar tidak berjalan dua kali
            Destroy(gameObject); 
        }
        else
        {
            Debug.LogWarning("Objek yang menyentuh BUKAN 'Player'. Pastikan Tag pada pemain sudah benar.");
        }
    }
}