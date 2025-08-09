using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk pindah scene
using UnityEngine.Audio;         // Diperlukan untuk Audio Mixer

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public AudioMixer audioMixer;

    // Fungsi untuk tombol START
    public void StartGame()
    {
        // Langsung memuat scene game utama Anda
        SceneManager.LoadScene("MainScene"); 
    }

    // Fungsi untuk tombol QUIT
    public void QuitGame()
    {
        Debug.Log("QUIT!"); // Untuk tes di editor
        Application.Quit();
    }

    // Fungsi untuk membuka panel settings
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Fungsi untuk menutup panel settings
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // --- FUNGSI UNTUK PENGATURAN ---

    public void SetVolume(float volume)
    {
        // "MasterVolume" adalah nama parameter yang akan kita buat di Audio Mixer
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}