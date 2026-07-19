using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField] private string tutorialMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (TutorialPopup.Instance != null)
            {
                TutorialPopup.Instance.ShowTutorial(tutorialMessage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (TutorialPopup.Instance != null)
            {
                TutorialPopup.Instance.HideTutorial();
            }
        }
    }
}
