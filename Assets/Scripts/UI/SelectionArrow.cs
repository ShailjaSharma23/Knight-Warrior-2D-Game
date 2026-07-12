using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

    private void Start()
    {
        for (int i = 0; i < options.Length; i++)
        {
            int index = i;
            Button btn = options[index].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() => SoundManager.instance.PlaySound(selectSound));
            }

            // mouse hover
            EventTrigger trigger = options[index].gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = options[index].gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => {
                OnPointerEnterOption(index);
            });
            trigger.triggers.Add(entry);
        }
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

    private void OnPointerEnterOption(int index)
    {
        if (currPos != index)
        {
            currPos = index;
            SoundManager.instance.PlaySound(changeSound);
            rect.position = new Vector3(rect.position.x, options[currPos].position.y, 0);
        }
    }

    private void Interact()
    {
        // sound will be played by onclick listener
        options[currPos].GetComponent<Button>().onClick.Invoke();
    }
}
