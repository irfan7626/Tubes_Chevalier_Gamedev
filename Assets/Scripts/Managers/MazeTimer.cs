using UnityEngine;
using TMPro; // Jangan lupa tambahkan ini jika memakai TextMeshPro

public class MazeTimer : MonoBehaviour
{
    public float timeRemaining = 180; // Waktu dalam detik
    public bool timerIsRunning = false;

    public TMP_Text timerText; // Seret objek UI Text-mu ke sini di Inspector

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Waktu Habis!");
                timeRemaining = 0;
                timerIsRunning = false;
                // Di sini Anda bisa memanggil fungsi Game Over
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay); // Pastikan waktu tidak negatif
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format string agar selalu jadi 00:00
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Fungsi untuk memulai timer (dipanggil oleh trigger di awal maze)
    public void StartTimer()
    {
        timerIsRunning = true;
    }

    // --- FUNGSI BARU UNTUK MENGHENTIKAN TIMER ---
    // Fungsi ini akan dipanggil oleh trigger di garis finis
    public void StopTimer()
    {
        if (timerIsRunning)
        {
            timerIsRunning = false;
            Debug.Log("MAZE SELESAI! Waktu berhenti di: " + timerText.text);

            // Di sini nanti Anda akan memanggil UI Endgame
        }
    }
}