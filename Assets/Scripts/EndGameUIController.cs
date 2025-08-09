using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUIController : MonoBehaviour
{
    /// <summary>
    /// Fungsi ini untuk kembali ke Main Menu.
    /// Pastikan nama scene "MainMenu" sudah benar.
    /// </summary>
    public void BackToMainMenu()
    {
        // Penting: Kembalikan Time.timeScale ke 1 jika Anda menggunakan pause
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Fungsi ini untuk me-restart level yang sedang berjalan.
    /// </summary>
    public void RestartLevel()
    {
        // Penting: Kembalikan Time.timeScale ke 1 jika Anda menggunakan pause
        Time.timeScale = 1f;
        
        // Mengambil nama scene yang sedang aktif saat ini
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Memuat ulang scene tersebut
        SceneManager.LoadScene(currentSceneName);
    }
}