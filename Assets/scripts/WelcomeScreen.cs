using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject infopanel;
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("main"); // Replace with your main game scene name
    }

    

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Quit button clicked");
    }

    public void showinfo()
    {
        infopanel.SetActive(true);
    }

    public void hideinfo()
    {
        infopanel.SetActive(false);
    }
}
