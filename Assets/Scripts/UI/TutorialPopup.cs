using UnityEngine;
using TMPro;

public class TutorialPopup : MonoBehaviour
{
    public static TutorialPopup Instance { get; private set; }

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void ShowTutorial(string message)
    {
        if (tutorialText != null)
        {
            tutorialText.text = message;
        }
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }
}
