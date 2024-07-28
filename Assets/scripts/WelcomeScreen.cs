using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreen : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("main"); // Replace with your main game scene name
    }

    public void OpenOptions()
    {
        // Open the options menu
        // This could involve enabling/disabling UI panels
        Debug.Log("Options button clicked");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Quit button clicked");
    }
}
