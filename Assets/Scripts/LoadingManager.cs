using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
        }
    }
}
