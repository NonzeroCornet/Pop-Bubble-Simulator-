using UnityEngine;
using UnityEngine.SceneManagement;

// ! PLEASE DONT TOUCH THIS FILE ITS JUST FOR SETTINGS BUTTTONS
public class SettingsManager : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
