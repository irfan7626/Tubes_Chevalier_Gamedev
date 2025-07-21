using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [System.Serializable]
    public class MenuButton
    {
        public Button button;
        public string targetScene;
    }

    public MenuButton[] menuButtons;

    void Start()
    {
        foreach (MenuButton mb in menuButtons)
        {
            if (mb.button != null && !string.IsNullOrEmpty(mb.targetScene))
            {
                mb.button.onClick.AddListener(() => LoadScene(mb.targetScene));
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}