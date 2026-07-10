using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currPos;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip selectSound;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

    }

    private void Update()
    {
        // change position of the arrow
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            ChangePosition(-1);
        if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            ChangePosition(1);

        // interact with options
        if (Keyboard.current.enterKey.wasPressedThisFrame)
            Interact();
    }

    private void ChangePosition(int change)
    {
        currPos += change;
        if (change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currPos < 0)
            currPos = options.Length - 1;
        else if (currPos >= options.Length)
            currPos = 0;
        rect.position = new Vector3(rect.position.x, options[currPos].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(selectSound);
        options[currPos].GetComponent<Button>().onClick.Invoke();
    }
}
