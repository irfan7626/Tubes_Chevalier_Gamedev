using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;
    public GameObject promptUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowPrompt(bool show)
    {
        promptUI.SetActive(show);
    }
}
