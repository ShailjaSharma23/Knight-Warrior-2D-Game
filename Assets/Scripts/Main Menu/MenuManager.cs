using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject levelsPage;
    [SerializeField] private GameObject analyticsPage;

    private void Awake()
    {
        settingsPage.SetActive(false);
        levelsPage.SetActive(false);
        analyticsPage.SetActive(false);
    }

    #region Main Menu

    public void StartGame()
    {
        levelsPage.SetActive(true);
    }

    public void Settings()
    {
        settingsPage.SetActive(true);
    }

    public void Analytics()
    {
        analyticsPage.SetActive(true);
    }

    #endregion


    #region Game Actions

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
    
}