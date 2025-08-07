using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Lanjutkan waktu
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Sembunyikan kursor lagi
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Hentikan waktu
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // Tampilkan kursor
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}