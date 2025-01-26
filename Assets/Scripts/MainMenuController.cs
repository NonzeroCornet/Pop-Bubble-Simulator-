using UnityEngine;
using UnityEngine.SceneManagement;

// ! This is just for main menu functions
public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData data = SaveSystem.Instance.GetData();
    }

    public void StartGame()
    {
        // Start the game
        SceneManager.LoadScene("Game");
    }

    public void EnterSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        // Exit the game
        Application.Quit();

        // Editor
        # if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        # endif
    }

}
