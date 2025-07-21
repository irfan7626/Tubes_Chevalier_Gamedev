using UnityEngine;
using TMPro; // Jangan lupa tambahkan ini untuk TextMeshPro

public class MazeTimer : MonoBehaviour
{
    public float timeRemaining = 180; // Waktu dalam detik, misal 180 detik = 3 menit
    public bool timerIsRunning = false;

    public TMP_Text timerText; // Seret objek UI Text-mu ke sini di Inspector

    void Start()
    {
        // Contoh: Timer akan mulai saat pemain masuk ke area maze
        // Untuk sekarang kita set true untuk tes
        // timerIsRunning = true; 
    }

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
                // Di sini kamu bisa memanggil fungsi Game Over
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Agar tidak langsung ke detik sebelumnya

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format string agar selalu jadi 00:00
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Fungsi ini bisa dipanggil dari skrip lain untuk memulai timer
    public void StartTimer()
    {
        timerIsRunning = true;
    }
}