using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 180;
    public bool timerIsRunning = false;
    public TMP_Text timerText;

    [Header("Audio Settings")]
    public AudioClip pantingSound;   // Suara ngos-ngosan yang berulang
    public AudioClip jumpscareSound; // Suara jumpscare (satu kali)
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
                Debug.Log("Waktu Habis! Me-restart level...");
                timeRemaining = 0;
                timerIsRunning = false;
                
                if (audioSource != null) audioSource.Stop();

                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        timerIsRunning = true;

        if (audioSource != null)
        {
            // --- BAGIAN BARU: Mainkan suara jumpscare satu kali ---
            if (jumpscareSound != null)
            {
                audioSource.PlayOneShot(jumpscareSound);
            }
            // ----------------------------------------------------

            // Mainkan suara ngos-ngosan secara berulang
            if (pantingSound != null)
            {
                audioSource.clip = pantingSound;
                audioSource.Play();
            }
        }
    }

    public void StopTimer()
    {
        if (timerIsRunning)
        {
            timerIsRunning = false;
            Debug.Log("MAZE SELESAI! Waktu berhenti di: " + timerText.text);

            if (audioSource != null) audioSource.Stop();
        }
    }
}